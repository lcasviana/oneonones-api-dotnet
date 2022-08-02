using Microsoft.Extensions.DependencyInjection;
using Oneonones.Service.Contracts;
using Oneonones.Service.Implementations;

namespace Oneonones.Infrastructure.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void ResolveDependencyInjections(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddServicesImplementations();
            serviceCollection.AddRepositoriesImplementations();
        }

        private static void AddServicesImplementations(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAccountsService, AccountsService>();
            serviceCollection.AddSingleton<IDashboardsService, DashboardsService>();
            serviceCollection.AddSingleton<IEmployeesService, EmployeesService>();
            serviceCollection.AddSingleton<IOneononesService, OneononesService>();
            serviceCollection.AddSingleton<IHistoricalsService, HistoricalsService>();
        }

        private static void AddRepositoriesImplementations(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAccountsRepository, AccountsRepository>();
            serviceCollection.AddSingleton<IEmployeesRepository, EmployeesRepository>();
            serviceCollection.AddSingleton<IOneononesRepository, OneononesRepository>();
            serviceCollection.AddSingleton<IHistoricalsRepository, HistoricalsRepository>();
        }
    }
}