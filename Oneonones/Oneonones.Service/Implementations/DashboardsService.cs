using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oneonones.Domain.Entities;
using Oneonones.Domain.Enums;
using Oneonones.Service.Contracts;

namespace Oneonones.Service.Implementations
{
    public class DashboardsService : IDashboardsService
    {
        private readonly IEmployeesService employeesService;
        private readonly IOneononesService oneononesService;
        private readonly IHistoricalsService historicalsService;

        public DashboardsService(
            IEmployeesService employeesService,
            IOneononesService oneononesService,
            IHistoricalsService historicalsService)
        {
            this.employeesService = employeesService;
            this.oneononesService = oneononesService;
            this.historicalsService = historicalsService;
        }

        public async Task<IList<DashboardEntity>> Obtain()
        {
            var employees = await employeesService.Obtain();
            var dashboardTask = employees.Select(ObtainEmployeeDashboard);
            var dashboardCompleted = await Task.WhenAll(dashboardTask);
            return dashboardCompleted.ToList();
        }

        public async Task<DashboardEntity> Obtain(string id)
        {
            var employee = await employeesService.Obtain(id);
            var dashboardEntity = await ObtainEmployeeDashboard(employee);
            return dashboardEntity;
        }

        private async Task<DashboardEntity> ObtainEmployeeDashboard(EmployeeEntity employee)
        {
            var oneononeList = await oneononesService.ObtainByEmployee(employee.Id);
            var oneononeComposeTask = oneononeList.Select(ObtainEmployeeOneononeCompose);
            var oneononeComposeComplete = await Task.WhenAll(oneononeComposeTask);
            var dashboardEntity = new DashboardEntity
            {
                Employee = employee,
                Oneonones = oneononeComposeComplete.ToList(),
            };
            return dashboardEntity;
        }

        private async Task<OneononeComposeEntity> ObtainEmployeeOneononeCompose(OneononeEntity oneonone)
        {
            StatusEntity status = null;
            var historical = await historicalsService.ObtainByPair(oneonone.Leader.Id, oneonone.Led.Id);
            if (historical.Any())
            {
                var lastOccurrence = historical.Max(h => h.Occurrence);
                var nextOccurrence = ObtainNextOccurrence(oneonone.Frequency, lastOccurrence);
                status = new StatusEntity
                {
                    LastOccurrence = lastOccurrence,
                    NextOccurrence = nextOccurrence,
                    IsLate = nextOccurrence.Date < DateTime.Now.Date,
                };
            }
            var oneononeComposeEntity = new OneononeComposeEntity
            {
                Oneonone = oneonone,
                Historical = historical,
                Status = status,
            };
            return oneononeComposeEntity;
        }

        private DateTime ObtainNextOccurrence(FrequencyEnum frequency, DateTime lastOccurrence)
        {
            return frequency switch
            {
                FrequencyEnum.Weekly => lastOccurrence.AddDays(7).Date,
                FrequencyEnum.Semimonthly => lastOccurrence.AddDays(14).Date,
                FrequencyEnum.Monthly => lastOccurrence.AddMonths(1).Date,
                FrequencyEnum.Bimonthly => lastOccurrence.AddMonths(2).Date,
                FrequencyEnum.Trimonthly => lastOccurrence.AddMonths(3).Date,
                FrequencyEnum.Semiyearly => lastOccurrence.AddMonths(6).Date,
                FrequencyEnum.Yearly => lastOccurrence.AddYears(1).Date,
                FrequencyEnum.Occasionally => DateTime.MaxValue.Date,
                _ => DateTime.MinValue,
            };
        }
    }
}