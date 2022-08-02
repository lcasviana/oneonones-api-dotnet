using FluentValidation;
using Oneonones.Domain.Inputs;
using Oneonones.Domain.Validators;

namespace Oneonones.Infrastructure;

public static class ValidatorsConfig
{
    public static void AddValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidator<EmployeeInput>, EmployeeValidator>();
        serviceCollection.AddScoped<IValidator<MeetingInput>, MeetingValidator>();
        serviceCollection.AddScoped<IValidator<OneononeInput>, OneononeValidator>();
    }
}
