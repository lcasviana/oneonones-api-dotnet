using Microsoft.Extensions.DependencyInjection;
using Oneonones.Persistence.Contracts.Databases;
using Oneonones.Persistence.Contracts.Repositories;
using Oneonones.Persistence.Databases;
using Oneonones.Persistence.Repositories;
using Oneonones.Service.Contracts;
using Oneonones.Service.Implementations;

namespace Oneonones.Infrastructure.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void ResolveDependencyInjections(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddServicesImplementations();
            serviceCollection.AddDatabasesImplementations();
            serviceCollection.AddRepositoriesImplementations();
        }

        private static void AddServicesImplementations(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IDashboardsService, DashboardsService>();
            serviceCollection.AddSingleton<IEmployeesService, EmployeesService>();
            serviceCollection.AddSingleton<IOneononesService, OneononesService>();
            serviceCollection.AddSingleton<IOneononesHistoricalService, OneononesHistoricalService>();
        }

        private static void AddDatabasesImplementations(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IEmployeesDatabase, EmployeesDatabase>();
            serviceCollection.AddSingleton<IOneononesDatabase, OneononesDatabase>();
            serviceCollection.AddSingleton<IHistoricalsDatabase, HistoricalsDatabase>();
        }

        private static void AddRepositoriesImplementations(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IEmployeesRepository, EmployeesRepository>();
            serviceCollection.AddSingleton<IOneononesRepository, OneononesRepository>();
            serviceCollection.AddSingleton<IHistoricalsRepository, HistoricalsRepository>();
        }
    }
}