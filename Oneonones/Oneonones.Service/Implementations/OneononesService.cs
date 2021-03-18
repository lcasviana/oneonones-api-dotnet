﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Oneonones.Domain.Entities;
using Oneonones.Domain.Enums;
using Oneonones.Domain.Messages;
using Oneonones.Persistence.Contracts.Repositories;
using Oneonones.Service.Contracts;
using Oneonones.Service.Exceptions;

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

        public async Task<IList<OneononeEntity>> Obtain()
        {
            var oneononeList = await oneononesRepository.Obtain();
            FillEmployees(oneononeList);
            return oneononeList;
        }

        public async Task<OneononeEntity> Obtain(string id)
        {
            if (Guid.TryParse(id, out var _))
                throw new ApiException(HttpStatusCode.BadRequest, GlobalMessages.InvalidId(id));

            var oneonone = await oneononesRepository.Obtain(id);
            if (oneonone == null)
                throw new ApiException(HttpStatusCode.NotFound, OneononesMessages.NotFound(id));

            await FillEmployees(oneonone);
            return oneonone;
        }

        public async Task<IList<OneononeEntity>> ObtainByEmployee(string id)
        {
            var employee = await employeesService.Obtain(id);

            var oneononeList = await oneononesRepository.ObtainByEmployee(id);
            if (oneononeList == null || !oneononeList.Any())
                throw new ApiException(HttpStatusCode.NotFound, OneononesMessages.Empty(employee.Email));

            FillEmployees(oneononeList);
            return oneononeList;
        }

        public async Task<OneononeEntity> ObtainByPair(string leaderId, string ledId)
        {
            var (leader, led) = await employeesService.ObtainPair(leaderId, ledId);

            var oneonone = await oneononesRepository.ObtainByPair(leaderId, ledId);
            if (oneonone == null)
                throw new ApiException(HttpStatusCode.NotFound, OneononesMessages.NotFound(leader.Email, led.Email));

            FillEmployees(oneonone, leader, led);
            return oneonone;
        }

        public async Task<OneononeEntity> Insert(OneononeInputEntity oneononeInput)
        {
            var (leader, led) = await employeesService.ObtainPair(oneononeInput?.LeaderId, oneononeInput?.LedId);

            if (!Enum.IsDefined(typeof(FrequencyEnum), oneononeInput.Frequency))
                throw new ApiException(HttpStatusCode.BadRequest, OneononesMessages.InvalidFrequency((int)oneononeInput.Frequency));

            var oneononeObtained = await oneononesRepository.ObtainByPair(leader.Id, led.Id);
            if (oneononeObtained != null)
                throw new ApiException(HttpStatusCode.Conflict, OneononesMessages.Conflict(leader.Email, led.Email));

            var oneonone = new OneononeEntity(leader, led, oneononeInput.Frequency);
            var inserted = await oneononesRepository.Insert(oneonone);
            if (!inserted)
                throw new ApiException(HttpStatusCode.InternalServerError, OneononesMessages.Insert(oneonone.Leader.Email, oneonone.Led.Email));

            return oneonone;
        }

        public async Task<OneononeEntity> Update(OneononeEntity oneonone)
        {
            var (leader, led) = await employeesService.ObtainPair(oneonone?.Leader?.Id, oneonone?.Led?.Id);

            if (!Enum.IsDefined(typeof(FrequencyEnum), oneonone.Frequency))
                throw new ApiException(HttpStatusCode.BadRequest, OneononesMessages.InvalidFrequency((int)oneonone.Frequency));

            var oneononeObtained = await oneononesRepository.Obtain(oneonone.Id);
            if (oneononeObtained == null)
                throw new ApiException(HttpStatusCode.NotFound, OneononesMessages.NotFound(leader.Email, led.Email));

            FillEmployees(oneonone, leader, led);
            var updated = await oneononesRepository.Update(oneonone);
            if (!updated)
                throw new ApiException(HttpStatusCode.InternalServerError, OneononesMessages.Update(oneonone.Leader.Email, oneonone.Led.Email));

            return oneonone;
        }

        public async Task Delete(string id)
        {
            if (Guid.TryParse(id, out var _))
                throw new ApiException(HttpStatusCode.BadRequest, GlobalMessages.InvalidId(id));

            var oneonone = await oneononesRepository.Obtain(id);
            if (oneonone == null)
                throw new ApiException(HttpStatusCode.NotFound, OneononesMessages.NotFound(id));

            var deleted = await oneononesRepository.Delete(id);
            if (!deleted)
                throw new ApiException(HttpStatusCode.InternalServerError, OneononesMessages.Delete(id));
        }

        private void FillEmployees(IList<OneononeEntity> oneononeList)
        {
            Parallel.For(0, oneononeList.Count, async i => await FillEmployees(oneononeList[i]));
        }

        private async Task FillEmployees(OneononeEntity oneonone)
        {
            var (leader, led) = await employeesService.ObtainPair(oneonone.Leader.Id, oneonone.Led.Id);
            FillEmployees(oneonone, leader, led);
        }

        private void FillEmployees(OneononeEntity oneonone, EmployeeEntity leader, EmployeeEntity led)
        {
            oneonone.Leader = leader;
            oneonone.Led = led;
        }
    }
}