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
            var dashboard = dashboardCompleted.OrderBy(d => d.Employee.Name).ToList();
            return dashboard;
        }

        public async Task<DashboardEntity> Obtain(string id)
        {
            var employee = await employeesService.Obtain(id);
            var dashboardEntity = await ObtainEmployeeDashboard(employee);
            return dashboardEntity;
        }

        public async Task<DashboardEntity> ObtainByEmail(string email)
        {
            var employee = await employeesService.ObtainByEmail(email);
            var dashboardEntity = await ObtainEmployeeDashboard(employee);
            return dashboardEntity;
        }

        private async Task<DashboardEntity> ObtainEmployeeDashboard(EmployeeEntity employee)
        {
            IList<OneononeEntity> oneononeList = new List<OneononeEntity>();
            try
            {
                oneononeList = await oneononesService.ObtainByEmployee(employee.Id);
            }
            catch
            {
                // supress
            }
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
            var historical = await ObtainHistoricalByPair(oneonone.Leader.Id, oneonone.Led.Id);
            StatusEntity status = ObtainStatusByHistorical(historical, oneonone.Frequency);

            var oneononeComposeEntity = new OneononeComposeEntity
            {
                Oneonone = oneonone,
                Historical = historical,
                Status = status,
            };
            return oneononeComposeEntity;
        }

        private async Task<IList<HistoricalEntity>> ObtainHistoricalByPair(string leaderId, string ledId)
        {
            try
            {
                var historical = await historicalsService.ObtainByPair(leaderId, ledId);
                return historical;
            }
            catch
            {
                return Array.Empty<HistoricalEntity>();
            }
        }

        private static StatusEntity ObtainStatusByHistorical(IList<HistoricalEntity> historical, FrequencyEnum frequency)
        {
            if (historical.Any())
            {
                var lastOccurrence = historical.Max(h => h.Occurrence);
                var nextOccurrence = ObtainNextOccurrence(frequency, lastOccurrence);

                var status = new StatusEntity
                {
                    LastOccurrence = lastOccurrence,
                    NextOccurrence = nextOccurrence,
                    IsLate = nextOccurrence.Date < DateTime.Now.Date,
                };
                return status;
            }

            return null;
        }

        private static DateTime ObtainNextOccurrence(FrequencyEnum frequency, DateTime lastOccurrence)
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
