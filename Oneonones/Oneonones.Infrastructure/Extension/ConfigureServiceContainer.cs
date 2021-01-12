using Oneonones.Persistence.Contract;
using Oneonones.Persistence.Databases;
using Oneonones.Service.Contract;
using Oneonones.Service.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Oneonones.Infrastructure.Extension
{
    public static class ConfigureServiceContainer
    {
        public static void AddServicesImplementations(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IOneononesService, OneononesService>();
        }

        public static void AddPersistencesImplementations(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IOneononesDatabase, OneononesDatabase>();
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