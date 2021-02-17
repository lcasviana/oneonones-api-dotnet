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

            _ = await employeesService.Obtain(email);

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

            var oneononeHistorical = (await oneononesHistoricalRepository.ObtainByPairOccurrence(leaderEmail, ledEmail, occurrence))
                ?? throw new ApiException(HttpStatusCode.NotFound, OneononesHistoricalMessages.NotFound, leaderEmail, ledEmail, occurrence.ToString("dd-MM-yyyy"));

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
                throw new ApiException(HttpStatusCode.Conflict, OneononesHistoricalMessages.Conflict,
                oneononeHistoricalInput.LeaderEmail, oneononeHistoricalInput.LedEmail, oneononeHistoricalInput.Occurrence.ToString("dd-MM-yyyy"));

            var oneononeHistoricalComplete = NewEntity(leader, led, oneononeHistoricalInput.Occurrence, oneononeHistoricalInput.Commentary);
            var inserted = await oneononesHistoricalRepository.Insert(oneononeHistoricalComplete);
            if (!inserted) throw new ApiException(HttpStatusCode.InternalServerError, OneononesHistoricalMessages.Insert,
                oneononeHistoricalInput.LeaderEmail, oneononeHistoricalInput.LedEmail, oneononeHistoricalInput.Occurrence.ToString("dd-MM-yyyy"));
        }

        public async Task Update(OneononeHistoricalInputEntity oneononeHistoricalInput)
        {
            var (leader, led) = await employeesService.ObtainPair(oneononeHistoricalInput.LeaderEmail, oneononeHistoricalInput.LedEmail);

            if (oneononeHistoricalInput.Occurrence == DateTime.MinValue)
                throw new ApiException(HttpStatusCode.BadRequest, OneononesHistoricalMessages.InvalidOccurrence);

            _ = (await oneononesHistoricalRepository.ObtainByPairOccurrence(
                oneononeHistoricalInput.LeaderEmail, oneononeHistoricalInput.LedEmail, oneononeHistoricalInput.Occurrence))
                ?? throw new ApiException(HttpStatusCode.NotFound, OneononesHistoricalMessages.NotFound,
                oneononeHistoricalInput.LeaderEmail, oneononeHistoricalInput.LedEmail, oneononeHistoricalInput.Occurrence.ToString("dd-MM-yyyy"));

            var oneononeHistoricalComplete = NewEntity(leader, led, oneononeHistoricalInput.Occurrence, oneononeHistoricalInput.Commentary);
            var updated = await oneononesHistoricalRepository.Update(oneononeHistoricalComplete);
            if (!updated) throw new ApiException(HttpStatusCode.InternalServerError, OneononesHistoricalMessages.Update,
                oneononeHistoricalInput.LeaderEmail, oneononeHistoricalInput.LedEmail, oneononeHistoricalInput.Occurrence.ToString("dd-MM-yyyy"));
        }

        public async Task Delete(string leaderEmail, string ledEmail, DateTime occurrence)
        {
            _ = await employeesService.ObtainPair(leaderEmail, ledEmail);

            _ = (await oneononesHistoricalRepository.ObtainByPairOccurrence(leaderEmail, ledEmail, occurrence))
                ?? throw new ApiException(HttpStatusCode.NotFound, OneononesHistoricalMessages.NotFound, leaderEmail, ledEmail, occurrence.ToString("dd-MM-yyyy"));

            var deleted = await oneononesHistoricalRepository.Delete(leaderEmail, ledEmail, occurrence);
            if (!deleted) throw new ApiException(HttpStatusCode.InternalServerError, OneononesHistoricalMessages.Delete, leaderEmail, ledEmail, occurrence.ToString("dd-MM-yyyy"));
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