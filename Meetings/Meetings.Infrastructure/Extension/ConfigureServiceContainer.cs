using Meetings.Persistence.Contract;
using Meetings.Persistence.Databases;
using Meetings.Service.Contract;
using Meetings.Service.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Meetings.Infrastructure.Extension
{
    public static class ConfigureServiceContainer
    {
        public static void AddServicesImplementations(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMeetingsService, MeetingsService>();
        }

        public static void AddPersistencesImplementations(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMeetingsDatabase, MeetingsDatabase>();
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