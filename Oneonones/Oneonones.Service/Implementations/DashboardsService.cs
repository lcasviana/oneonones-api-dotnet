using Oneonones.Domain.Entities;
using Oneonones.Domain.Enums;
using Oneonones.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oneonones.Service.Implementations
{
    public class DashboardsService : IDashboardsService
    {
        private readonly IEmployeesService employeesService;
        private readonly IOneononesService oneononesService;
        private readonly IOneononesHistoricalService oneononesHistoricalService;

        public DashboardsService(
            IEmployeesService employeesService,
            IOneononesService oneononesService,
            IOneononesHistoricalService oneononesHistoricalService)
        {
            this.employeesService = employeesService;
            this.oneononesService = oneononesService;
            this.oneononesHistoricalService = oneononesHistoricalService;
        }

        public async Task<IList<DashboardEntity>> ObtainAll()
        {
            var employees = await employeesService.ObtainAll();
            var dashboardTask = employees.Select(ObtainEmployeeDashboard);
            var dashboardCompleted = await Task.WhenAll(dashboardTask);
            return dashboardCompleted.ToList();
        }

        public async Task<DashboardEntity> ObtainByEmployee(string email)
        {
            var employee = await employeesService.Obtain(email);
            var dashboardEntity = await ObtainEmployeeDashboard(employee);
            return dashboardEntity;
        }

        private async Task<DashboardEntity> ObtainEmployeeDashboard(EmployeeEntity employee)
        {
            var oneononeList = await oneononesService.ObtainByEmployee(employee.Email);
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
            OneononeStatusEntity status = null;
            var historical = await oneononesHistoricalService.ObtainByPair(oneonone.Leader.Email, oneonone.Led.Email);
            if (historical.Any())
            {
                var lastOccurrence = historical.Max(h => h.Occurrence);
                var nextOccurrence = ObtainNextOccurrence(oneonone.Frequency, lastOccurrence);
                status = new OneononeStatusEntity
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

        private DateTime ObtainNextOccurrence(OneononeFrequencyEnum frequency, DateTime lastOccurrence)
        {
            return frequency switch
            {
                OneononeFrequencyEnum.Weekly => lastOccurrence.AddDays(7).Date,
                OneononeFrequencyEnum.Semimonthly => lastOccurrence.AddDays(14).Date,
                OneononeFrequencyEnum.Monthly => lastOccurrence.AddMonths(1).Date,
                OneononeFrequencyEnum.Bimonthly => lastOccurrence.AddMonths(2).Date,
                OneononeFrequencyEnum.Trimonthly => lastOccurrence.AddMonths(3).Date,
                OneononeFrequencyEnum.Semiyearly => lastOccurrence.AddMonths(6).Date,
                OneononeFrequencyEnum.Yearly => lastOccurrence.AddYears(1).Date,
                OneononeFrequencyEnum.Occasionally => DateTime.MaxValue.Date,
                _ => DateTime.MinValue,
            };
        }
    }
}