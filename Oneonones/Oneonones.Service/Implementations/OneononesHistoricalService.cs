using Oneonones.Domain.Entities;
using Oneonones.Domain.Messages;
using Oneonones.Persistence.Contracts.Repositories;
using Oneonones.Service.Contracts;
using Oneonones.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Oneonones.Service.Implementations
{
    public class OneononesHistoricalService : IOneononesHistoricalService
    {
        private readonly IOneononesHistoricalRepository oneononesHistoricalRepository;
        private readonly IEmployeesService employeesService;

        public OneononesHistoricalService(
            IOneononesHistoricalRepository oneononesHistoricalRepository,
            IEmployeesService employeesService)
        {
            this.oneononesHistoricalRepository = oneononesHistoricalRepository;
            this.employeesService = employeesService;
        }

        public async Task<IList<OneononeHistoricalEntity>> ObtainAll()
        {
            var oneononeHistoricalList = await oneononesHistoricalRepository.ObtainAll();
            var oneononeHistoricalListComplete = await CompleteEntityList(oneononeHistoricalList);
            return oneononeHistoricalListComplete;
        }

        public async Task<IList<OneononeHistoricalEntity>> ObtainByEmployee(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ApiException(HttpStatusCode.BadRequest, EmployeesMessages.InvalidEmail);

            var oneononeHistoricalList = await oneononesHistoricalRepository.ObtainByEmployee(email);
            var oneononeHistoricalListComplete = await CompleteEntityList(oneononeHistoricalList);
            return oneononeHistoricalListComplete;
        }

        public async Task<IList<OneononeHistoricalEntity>> ObtainByPair(string leaderEmail, string ledEmail)
        {
            var (leader, led) = await employeesService.ObtainPair(leaderEmail, ledEmail);

            var oneononeHistoricalList = await oneononesHistoricalRepository.ObtainByPair(leaderEmail, ledEmail);
            var oneononeHistoricalListComplete = oneononeHistoricalList.Select(h => NewEntity(leader, led, h.Occurrence, h.Commentary)).ToList();
            return oneononeHistoricalListComplete;
        }

        public async Task<OneononeHistoricalEntity> ObtainByPairOccurrence(string leaderEmail, string ledEmail, DateTime occurrence)
        {
            var (leader, led) = await employeesService.ObtainPair(leaderEmail, ledEmail);

            if (occurrence == DateTime.MinValue)
                throw new ApiException(HttpStatusCode.BadRequest, OneononesHistoricalMessages.InvalidOccurrence);

            var oneononeHistorical = await oneononesHistoricalRepository.ObtainByPairOccurrence(leaderEmail, ledEmail, occurrence);
            var oneononeHistoricalComplete = NewEntity(leader, led, oneononeHistorical.Occurrence, oneononeHistorical.Commentary);
            return oneononeHistoricalComplete;
        }

        public async Task Insert(OneononeHistoricalInputEntity oneononeHistoricalInput)
        {
            var (leader, led) = await employeesService.ObtainPair(oneononeHistoricalInput.LeaderEmail, oneononeHistoricalInput.LedEmail);

            if (oneononeHistoricalInput.Occurrence == DateTime.MinValue)
                throw new ApiException(HttpStatusCode.BadRequest, OneononesHistoricalMessages.InvalidOccurrence);

            var oneononeHistoricalObtained = await oneononesHistoricalRepository.ObtainByPairOccurrence(
               oneononeHistoricalInput.LeaderEmail, oneononeHistoricalInput.LedEmail, oneononeHistoricalInput.Occurrence);
            if (oneononeHistoricalObtained != null)
                throw new ApiException(HttpStatusCode.Conflict, OneononesHistoricalMessages.Conflict);

            var oneononeHistoricalComplete = NewEntity(leader, led, oneononeHistoricalInput.Occurrence, oneononeHistoricalInput.Commentary);
            await oneononesHistoricalRepository.Insert(oneononeHistoricalComplete);
        }

        public async Task Update(OneononeHistoricalInputEntity oneononeHistoricalInput)
        {
            var (leader, led) = await employeesService.ObtainPair(oneononeHistoricalInput.LeaderEmail, oneononeHistoricalInput.LedEmail);

            if (oneononeHistoricalInput.Occurrence == DateTime.MinValue)
                throw new ApiException(HttpStatusCode.BadRequest, OneononesHistoricalMessages.InvalidOccurrence);

            _ = (await oneononesHistoricalRepository.ObtainByPairOccurrence(
                oneononeHistoricalInput.LeaderEmail, oneononeHistoricalInput.LedEmail, oneononeHistoricalInput.Occurrence))
                ?? throw new ApiException(HttpStatusCode.NotFound, OneononesHistoricalMessages.NotFound);

            var oneononeHistoricalComplete = NewEntity(leader, led, oneononeHistoricalInput.Occurrence, oneononeHistoricalInput.Commentary);
            await oneononesHistoricalRepository.Update(oneononeHistoricalComplete);
        }

        public async Task Delete(string leaderEmail, string ledEmail, DateTime occurrence)
        {
            _ = await employeesService.ObtainPair(leaderEmail, ledEmail);

            await oneononesHistoricalRepository.Delete(leaderEmail, ledEmail, occurrence);
        }

        private async Task<IList<OneononeHistoricalEntity>> CompleteEntityList(IList<OneononeHistoricalEntity> oneononeHistoricalList)
        {
            var oneononeHistoricalTasks = oneononeHistoricalList.Select(async h =>
            {
                var (leader, led) = await employeesService.ObtainPair(h.Leader.Email, h.Led.Email);
                return NewEntity(leader, led, h.Occurrence, h.Commentary);
            });
            var oneononeHistoricalComplete = await Task.WhenAll(oneononeHistoricalTasks);
            return oneononeHistoricalComplete.ToList();
        }

        private OneononeHistoricalEntity NewEntity(EmployeeEntity leader, EmployeeEntity led, DateTime occurrence, string commentary)
        {
            return new OneononeHistoricalEntity
            {
                Leader = leader,
                Led = led,
                Occurrence = occurrence,
                Commentary = commentary,
            };
        }
    }
}