using Oneonones.Services;
using Oneonones.Services.Contracts;

namespace Oneonones.Infrastructure.Configurations;

public static class ServiceConfig
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IEmployeeService, EmployeeService>();
        serviceCollection.AddTransient<IMeetingService, MeetingService>();
        serviceCollection.AddTransient<IOneononeService, OneononeService>();
    }
}
