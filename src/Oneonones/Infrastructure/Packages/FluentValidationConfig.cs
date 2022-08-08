using FluentValidation;
using Oneonones.Domain.Inputs;
using Oneonones.Domain.Validators;

namespace Oneonones.Infrastructure.Packages;

public static class FluentValidationConfig
{
    public static void AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<EmployeeInput>, EmployeeValidator>();
        services.AddScoped<IValidator<MeetingInsert>, MeetingInsertValidator>();
        services.AddScoped<IValidator<MeetingUpdate>, MeetingUpdateValidator>();
        services.AddScoped<IValidator<OneononeInsert>, OneononeInsertValidator>();
        services.AddScoped<IValidator<OneononeUpdate>, OneononeUpdateValidator>();
    }
}
