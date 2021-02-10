using Oneonones.Domain.Entities;
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

        public async Task<IList<OneononeHistoricalEntity>> ObtainAll(string leaderEmail, string ledEmail)
        {
            var (leader, led) = await employeesService.ObtainPair(leaderEmail, ledEmail);

            var oneononeHistoricalList = await oneononesHistoricalRepository.ObtainAll(leaderEmail, ledEmail);

            oneononeHistoricalList = oneononeHistoricalList.Select(h =>
            {
                h.Leader = leader;
                h.Led = led;
                return h;
            }).ToList();

            return oneononeHistoricalList;
        }

        public async Task<OneononeHistoricalEntity> ObtainOccurrence(string leaderEmail, string ledEmail, DateTime occurrence)
        {
            var (leader, led) = await employeesService.ObtainPair(leaderEmail, ledEmail);

            if (occurrence == DateTime.MinValue)
                throw new ApiException(HttpStatusCode.BadRequest);

            var oneononeHistorical = await oneononesHistoricalRepository.ObtainOccurrence(leaderEmail, ledEmail, occurrence);

            oneononeHistorical.Leader = leader;
            oneononeHistorical.Led = led;

            return oneononeHistorical;
        }

        public async Task Insert(OneononeHistoricalInputEntity oneononeHistoricalInput)
        {
            var (leader, led) = await employeesService.ObtainPair(oneononeHistoricalInput.LeaderEmail, oneononeHistoricalInput.LedEmail);

            if (oneononeHistoricalInput.Occurrence == DateTime.MinValue)
                throw new ApiException(HttpStatusCode.BadRequest);

            var oneononeHistoricalObtained = await oneononesHistoricalRepository.ObtainOccurrence(
               oneononeHistoricalInput.LeaderEmail, oneononeHistoricalInput.LedEmail, oneononeHistoricalInput.Occurrence);
            if (oneononeHistoricalObtained != null)
                throw new ApiException(HttpStatusCode.Conflict);

            var oneononeHistorical = new OneononeHistoricalEntity
            {
                Leader = leader,
                Led = led,
                Occurrence = oneononeHistoricalInput.Occurrence,
                Commentary = oneononeHistoricalInput.Commentary,
            };

            await oneononesHistoricalRepository.Insert(oneononeHistorical);
        }

        public async Task Update(OneononeHistoricalInputEntity oneononeHistoricalInput)
        {
            var (leader, led) = await employeesService.ObtainPair(oneononeHistoricalInput.LeaderEmail, oneononeHistoricalInput.LedEmail);

            if (oneononeHistoricalInput.Occurrence == DateTime.MinValue)
                throw new ApiException(HttpStatusCode.BadRequest);

            var oneononeHistorical = (await oneononesHistoricalRepository.ObtainOccurrence(
                oneononeHistoricalInput.LeaderEmail, oneononeHistoricalInput.LedEmail, oneononeHistoricalInput.Occurrence))
                ?? throw new ApiException(HttpStatusCode.NotFound);

            oneononeHistorical.Leader = leader;
            oneononeHistorical.Led = led;
            oneononeHistorical.Occurrence = oneononeHistoricalInput.Occurrence;
            oneononeHistorical.Commentary = oneononeHistoricalInput.Commentary;

            await oneononesHistoricalRepository.Update(oneononeHistorical);
        }

        public async Task Delete(string leaderEmail, string ledEmail, DateTime occurrence)
        {
            _ = await employeesService.ObtainPair(leaderEmail, ledEmail);

            await oneononesHistoricalRepository.Delete(leaderEmail, ledEmail, occurrence);
        }
    }
}