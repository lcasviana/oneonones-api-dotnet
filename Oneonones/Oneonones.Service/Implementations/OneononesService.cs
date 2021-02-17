using Oneonones.Domain.Entities;
using Oneonones.Domain.Enums;
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

        public async Task<IList<OneononeEntity>> ObtainAll()
        {
            var oneononeList = await oneononesRepository.ObtainAll();
            var oneononeListComplete = await CompleteEntityList(oneononeList);
            return oneononeListComplete;
        }

        public async Task<IList<OneononeEntity>> ObtainByEmployee(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ApiException(HttpStatusCode.BadRequest, EmployeesMessages.InvalidEmail);

            _ = await employeesService.Obtain(email);

            var oneononeList = await oneononesRepository.ObtainByEmployee(email);
            var oneononeListComplete = await CompleteEntityList(oneononeList);
            return oneononeListComplete;
        }

        public async Task<OneononeEntity> ObtainByPair(string leaderEmail, string ledEmail)
        {
            var (leader, led) = await employeesService.ObtainPair(leaderEmail, ledEmail);

            var oneonone = (await oneononesRepository.ObtainByPair(leaderEmail, ledEmail))
                ?? throw new ApiException(HttpStatusCode.NotFound, OneononesMessages.NotFound, leaderEmail, ledEmail);

            return NewEntity(leader, led, oneonone.Frequency);
        }

        public async Task Insert(OneononeInputEntity oneononeInput)
        {
            var (leader, led) = await employeesService.ObtainPair(oneononeInput?.LeaderEmail, oneononeInput?.LedEmail);

            if (!Enum.IsDefined(typeof(OneononeFrequencyEnum), oneononeInput.Frequency))
                throw new ApiException(HttpStatusCode.BadRequest, OneononesMessages.InvalidFrequency, oneononeInput.Frequency);

            var oneononeObtained = await oneononesRepository.ObtainByPair(oneononeInput.LeaderEmail, oneononeInput.LedEmail);
            if (oneononeObtained != null)
                throw new ApiException(HttpStatusCode.Conflict, OneononesMessages.Conflict, oneononeInput.LeaderEmail, oneononeInput.LedEmail);

            var inserted = await oneononesRepository.Insert(NewEntity(leader, led, oneononeInput.Frequency));
            if (!inserted) throw new ApiException(HttpStatusCode.InternalServerError, OneononesMessages.Insert, oneononeInput.LeaderEmail, oneononeInput.LedEmail);
        }

        public async Task Update(OneononeInputEntity oneononeInput)
        {
            var (leader, led) = await employeesService.ObtainPair(oneononeInput?.LeaderEmail, oneononeInput?.LedEmail);

            if (!Enum.IsDefined(typeof(OneononeFrequencyEnum), oneononeInput.Frequency))
                throw new ApiException(HttpStatusCode.BadRequest, OneononesMessages.InvalidFrequency, oneononeInput.Frequency);

            _ = (await oneononesRepository.ObtainByPair(oneononeInput.LeaderEmail, oneononeInput.LedEmail))
                ?? throw new ApiException(HttpStatusCode.NotFound, OneononesMessages.NotFound, oneononeInput.LeaderEmail, oneononeInput.LedEmail);

            var updated = await oneononesRepository.Update(NewEntity(leader, led, oneononeInput.Frequency));
            if (!updated) throw new ApiException(HttpStatusCode.InternalServerError, OneononesMessages.Update, oneononeInput.LeaderEmail, oneononeInput.LedEmail);
        }

        public async Task Delete(string leaderEmail, string ledEmail)
        {
            _ = await employeesService.ObtainPair(leaderEmail, ledEmail);

            _ = (await oneononesRepository.ObtainByPair(leaderEmail, ledEmail))
                ?? throw new ApiException(HttpStatusCode.NotFound, OneononesMessages.NotFound, leaderEmail, ledEmail);

            var deleted = await oneononesRepository.Delete(leaderEmail, ledEmail);
            if (!deleted) throw new ApiException(HttpStatusCode.InternalServerError, OneononesMessages.Delete, leaderEmail, ledEmail);
        }

        private async Task<IList<OneononeEntity>> CompleteEntityList(IList<OneononeEntity> oneononelList)
        {
            var oneononeTasks = oneononelList.Select(async o =>
            {
                var (leader, led) = await employeesService.ObtainPair(o.Leader.Email, o.Led.Email);
                return NewEntity(leader, led, o.Frequency);
            });

            var oneononeComplete = await Task.WhenAll(oneononeTasks);
            return oneononeComplete.ToList();
        }

        private OneononeEntity NewEntity(EmployeeEntity leader, EmployeeEntity led, OneononeFrequencyEnum frequency)
        {
            return new OneononeEntity
            {
                Leader = leader,
                Led = led,
                Frequency = frequency,
            };
        }
    }
}