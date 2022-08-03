using FluentValidation;
using Oneonones.Domain.Inputs;
using Oneonones.Domain.Validators;

namespace Oneonones.Infrastructure;

public static class ValidatorsConfig
{
    public static void AddValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IValidator<EmployeeInput>, EmployeeValidator>();
        serviceCollection.AddSingleton<IValidator<MeetingInput>, MeetingValidator>();
        serviceCollection.AddSingleton<IValidator<OneononeInput>, OneononeValidator>();
    }
}
