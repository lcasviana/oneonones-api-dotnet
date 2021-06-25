using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Oneonones.Domain.Entities;
using Oneonones.Domain.Messages;
using Oneonones.Persistence.Contracts.Repositories;
using Oneonones.Service.Contracts;
using Oneonones.Service.Exceptions;

namespace Oneonones.Service.Implementations
{
    public class HistoricalsService : IHistoricalsService
    {
        private readonly IHistoricalsRepository historicalsRepository;
        private readonly IEmployeesService employeesService;
        private readonly IOneononesService oneononesService;

        public HistoricalsService(
            IHistoricalsRepository historicalsRepository,
            IEmployeesService employeesService,
            IOneononesService oneononesService)
        {
            this.historicalsRepository = historicalsRepository;
            this.employeesService = employeesService;
            this.oneononesService = oneononesService;
        }

        public async Task<IList<HistoricalEntity>> Obtain()
        {
            var historicalList = await historicalsRepository.Obtain();
            await FillEmployees(historicalList);
            return historicalList.OrderByDescending(h => h.Occurrence).ToList();
        }

        public async Task<HistoricalEntity> Obtain(string id)
        {
            if (!Guid.TryParse(id, out var _))
                throw new ApiException(HttpStatusCode.BadRequest, GlobalMessages.InvalidId(id));

            var historical = await historicalsRepository.Obtain(id);
            if (historical == null)
                throw new ApiException(HttpStatusCode.NotFound, HistoricalsMessages.NotFound(id));

            await FillEmployees(historical);
            return historical;
        }

        public async Task<IList<HistoricalEntity>> ObtainByEmployee(string id)
        {
            var employee = await employeesService.Obtain(id);

            var historicalList = await historicalsRepository.ObtainByEmployee(id);
            if (historicalList == null || !historicalList.Any())
                throw new ApiException(HttpStatusCode.NotFound, HistoricalsMessages.Empty(employee.Email));

            await FillEmployees(historicalList);
            return historicalList.OrderByDescending(h => h.Occurrence).ToList();
        }

        public async Task<IList<HistoricalEntity>> ObtainByPair(string leaderId, string ledId)
        {
            var (leader, led) = await employeesService.ObtainPair(leaderId, ledId);

            var historicalList = await historicalsRepository.ObtainByPair(leaderId, ledId);
            if (historicalList == null || !historicalList.Any())
                throw new ApiException(HttpStatusCode.NotFound, HistoricalsMessages.Empty(leader.Email, led.Email));

            await FillEmployees(historicalList);
            return historicalList.OrderByDescending(h => h.Occurrence).ToList();
        }

        public async Task<HistoricalEntity> ObtainByPairLast(string leaderId, string ledId)
        {
            var (leader, led) = await employeesService.ObtainPair(leaderId, ledId);

            var historicalList = await historicalsRepository.ObtainByPair(leaderId, ledId);
            if (historicalList == null || !historicalList.Any())
                throw new ApiException(HttpStatusCode.NotFound, HistoricalsMessages.Empty(leader.Email, led.Email));

            var occurrenceLast = historicalList.Max(historical => historical.Occurrence);
            var historicalLast = historicalList.FirstOrDefault(historical => historical.Occurrence.Date == occurrenceLast.Date);
            FillEmployees(historicalLast, leader, led);
            return historicalLast;
        }

        public async Task<HistoricalEntity> ObtainByOccurrence(string leaderId, string ledId, DateTime occurrence)
        {
            var (leader, led) = await employeesService.ObtainPair(leaderId, ledId);

            if (occurrence == DateTime.MinValue)
                throw new ApiException(HttpStatusCode.BadRequest, HistoricalsMessages.InvalidOccurrence(occurrence));

            var historicalList = await historicalsRepository.ObtainByPair(leaderId, ledId);
            if (historicalList == null || !historicalList.Any())
                throw new ApiException(HttpStatusCode.NotFound, HistoricalsMessages.Empty(leader.Email, led.Email));

            var occurrenceObtained = historicalList.FirstOrDefault(historical => historical.Occurrence.Date == occurrence.Date);
            if (occurrenceObtained == null)
                throw new ApiException(HttpStatusCode.NotFound, HistoricalsMessages.NotFound(leaderId, ledId, occurrence));

            FillEmployees(occurrenceObtained, leader, led);
            return occurrenceObtained;
        }

        public async Task<HistoricalEntity> Insert(HistoricalInputEntity historicalInput)
        {
            if (historicalInput.Occurrence == DateTime.MinValue)
                throw new ApiException(HttpStatusCode.BadRequest, HistoricalsMessages.InvalidOccurrence(historicalInput.Occurrence));

            var (leader, led) = await employeesService.ObtainPair(historicalInput?.LeaderId, historicalInput?.LedId);

            var historicalList = await historicalsRepository.ObtainByPair(historicalInput.LeaderId, historicalInput.LedId);
            var occurrenceObtained = historicalList?.FirstOrDefault(historical => historical.Occurrence.Date == historicalInput.Occurrence.Date);
            if (occurrenceObtained != null)
                throw new ApiException(HttpStatusCode.Conflict, HistoricalsMessages.Conflict(leader.Email, led.Email, historicalInput.Occurrence));

            _ = await oneononesService.ObtainByPair(leader.Id, led.Id);

            var historical = new HistoricalEntity(leader, led, historicalInput.Occurrence, historicalInput.Commentary);
            var inserted = await historicalsRepository.Insert(historical);
            if (!inserted)
                throw new ApiException(HttpStatusCode.InternalServerError, HistoricalsMessages.Insert(historical.Leader.Email, historical.Led.Email, historical.Occurrence));

            return historical;
        }

        public async Task<HistoricalEntity> Update(HistoricalEntity historical)
        {
            var requestErrors = new string[]
            {
                Guid.TryParse(historical.Id, out var _) ? null : GlobalMessages.InvalidId(historical.Id),
                historical.Occurrence == DateTime.MinValue ? HistoricalsMessages.InvalidOccurrence(historical.Occurrence) : null,
            }.Where(e => e != null);
            if (requestErrors.Any())
                throw new ApiException(HttpStatusCode.BadRequest, requestErrors.ToList());

            var (leader, led) = await employeesService.ObtainPair(historical?.Leader?.Id, historical?.Led?.Id);

            var historicalObtained = await historicalsRepository.Obtain(historical.Id);
            var historicalList = await historicalsRepository.ObtainByPair(historical.Leader.Id, historical.Led.Id);
            var occurrenceObtained = historicalList?.FirstOrDefault(historical => historical.Occurrence.Date == historical.Occurrence.Date);
            if (historicalObtained == null)
                throw new ApiException(HttpStatusCode.NotFound, HistoricalsMessages.NotFound(historical.Id));
            if (occurrenceObtained != null && historical.Id != occurrenceObtained.Id)
                throw new ApiException(HttpStatusCode.NotFound, HistoricalsMessages.Conflict(leader.Email, led.Email, historical.Occurrence));

            _ = await oneononesService.ObtainByPair(leader.Id, led.Id);

            FillEmployees(historical, leader, led);
            var updated = await historicalsRepository.Update(historical);
            if (!updated)
                throw new ApiException(HttpStatusCode.InternalServerError, HistoricalsMessages.Update(leader.Email, led.Email, historical.Occurrence));

            return historical;
        }

        public async Task Delete(string id)
        {
            if (!Guid.TryParse(id, out var _))
                throw new ApiException(HttpStatusCode.BadRequest, GlobalMessages.InvalidId(id));

            var historical = await historicalsRepository.Obtain(id);
            if (historical == null)
                throw new ApiException(HttpStatusCode.NotFound, HistoricalsMessages.NotFound(id));

            var deleted = await historicalsRepository.Delete(id);
            if (!deleted)
                throw new ApiException(HttpStatusCode.InternalServerError, HistoricalsMessages.Delete(id));
        }

        private void FillEmployees(IList<HistoricalEntity> historicalList, EmployeeEntity leader, EmployeeEntity led)
        {
            Parallel.For(0, historicalList.Count, i => FillEmployees(historicalList[i], leader, led));
        }

        private async Task FillEmployees(IList<HistoricalEntity> historicalList)
        {
            await Task.WhenAll(historicalList.Select(async historical => await FillEmployees(historical)));
        }

        private async Task FillEmployees(HistoricalEntity historical)
        {
            var (leader, led) = await employeesService.ObtainPair(historical.Leader.Id, historical.Led.Id);
            FillEmployees(historical, leader, led);
        }

        private void FillEmployees(HistoricalEntity historical, EmployeeEntity leader, EmployeeEntity led)
        {
            historical.Leader = leader;
            historical.Led = led;
        }
    }
}