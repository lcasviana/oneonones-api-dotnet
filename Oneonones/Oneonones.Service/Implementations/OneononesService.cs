using Oneonones.Domain.Entities;
using Oneonones.Domain.Enums;
using Oneonones.Persistence.Contracts.Repositories;
using Oneonones.Service.Contracts;
using Oneonones.Service.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Oneonones.Service.Implementations
{
    public class OneononesService : IOneononesService
    {
        private readonly IOneononesRepository oneononesRepository;
        private readonly IEmployeesService employeesService;

        public OneononesService(
            IOneononesRepository oneononesRepository,
            IEmployeesService employeesService)
        {
            this.employeesService = employeesService;
            this.oneononesRepository = oneononesRepository;
        }

        public async Task<OneononeEntity> ObtainByPair(string leaderEmail, string ledEmail)
        {
            var (leader, led) = await employeesService.ObtainPair(leaderEmail, ledEmail);

            var oneonone = (await oneononesRepository.ObtainByPair(leaderEmail, ledEmail))
                ?? throw new ApiException(HttpStatusCode.NotFound);

            oneonone.Leader = leader;
            oneonone.Led = led;

            return oneonone;
        }

        public async Task Insert(OneononeInputEntity oneononeInput)
        {
            var (leader, led) = await employeesService.ObtainPair(oneononeInput?.LeaderEmail, oneononeInput?.LedEmail);

            if (!Enum.IsDefined(typeof(OneononeFrequencyEnum), oneononeInput.Frequency))
                throw new ApiException(HttpStatusCode.BadRequest);

            var oneononeObtained = await oneononesRepository.ObtainByPair(oneononeInput.LeaderEmail, oneononeInput.LedEmail);
            if (oneononeObtained != null)
                throw new ApiException(HttpStatusCode.Conflict);

            var oneonone = new OneononeEntity
            {
                Leader = leader,
                Led = led,
                Frequency = oneononeInput.Frequency,
            };

            await oneononesRepository.Insert(oneonone);
        }

        public async Task Update(OneononeInputEntity oneononeInput)
        {
            var (leader, led) = await employeesService.ObtainPair(oneononeInput?.LeaderEmail, oneononeInput?.LedEmail);

            if (!Enum.IsDefined(typeof(OneononeFrequencyEnum), oneononeInput.Frequency))
                throw new ApiException(HttpStatusCode.BadRequest);

            var oneonone = (await oneononesRepository.ObtainByPair(oneononeInput.LeaderEmail, oneononeInput.LedEmail))
                ?? throw new ApiException(HttpStatusCode.NotFound);

            oneonone.Leader = leader;
            oneonone.Led = led;
            oneonone.Frequency = oneononeInput.Frequency;

            await oneononesRepository.Update(oneonone);
        }

        public async Task Delete(string leaderEmail, string ledEmail)
        {
            _ = await employeesService.ObtainPair(leaderEmail, ledEmail);

            await oneononesRepository.Delete(leaderEmail, ledEmail);
        }
    }
}