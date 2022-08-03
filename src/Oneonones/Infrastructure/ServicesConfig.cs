using Oneonones.Services;
using Oneonones.Services.Contracts;

namespace Oneonones.Infrastructure;

public static class ServicesConfig
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IEmployeeService, EmployeeService>();
        serviceCollection.AddTransient<IMeetingService, MeetingService>();
        serviceCollection.AddTransient<IOneononeService, OneononeService>();
    }
}
