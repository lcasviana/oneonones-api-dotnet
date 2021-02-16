using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Oneonones.Persistence.Contracts.Databases;
using Oneonones.Persistence.Contracts.Repositories;
using Oneonones.Persistence.Databases;
using Oneonones.Persistence.Repositories;
using Oneonones.Service.Contracts;
using Oneonones.Service.Implementations;

namespace Oneonones.Infrastructure.Extension
{
    public static class ConfigureServiceContainer
    {
        public static void AddServicesImplementations(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IEmployeesService, EmployeesService>();
            serviceCollection.AddSingleton<IOneononesService, OneononesService>();
            serviceCollection.AddSingleton<IOneononesHistoricalService, OneononesHistoricalService>();
        }

        public static void AddPersistencesImplementations(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IEmployeesDatabase, EmployeesDatabase>();
            serviceCollection.AddSingleton<IOneononesDatabase, OneononesDatabase>();
            serviceCollection.AddSingleton<IOneononesHistoricalDatabase, OneononesHistoricalDatabase>();

            serviceCollection.AddSingleton<IEmployeesRepository, EmployeesRepository>();
            serviceCollection.AddSingleton<IOneononesRepository, OneononesRepository>();
            serviceCollection.AddSingleton<IOneononesHistoricalRepository, OneononesHistoricalRepository>();
        }

        public static void AddController(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddControllers().AddNewtonsoftJson();
        }

        public static void AddVersion(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }
    }
}