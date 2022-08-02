using FluentValidation;
using Oneonones.Domain.Inputs;

namespace Oneonones.Domain.Validators;

public class OneononeValidator : AbstractValidator<OneononeInput>
{
    public OneononeValidator()
    {
        RuleFor(oneonone => oneonone.LeaderId).NotEmpty();
        RuleFor(oneonone => oneonone.LedId).NotEmpty();
        When(oneonone => oneonone.Frequency is null, () => RuleFor(oneonone => oneonone.Frequency).NotEmpty())
            .Otherwise(() => RuleFor(oneonone => oneonone.Frequency).IsInEnum());
    }
}
