using FluentValidation;
using Oneonones.Domain.Inputs;

namespace Oneonones.Domain.Validators;

public class OneononeInsertValidator : AbstractValidator<OneononeInsert>
{
    public OneononeInsertValidator()
    {
        RuleFor(oneonone => oneonone.LeaderId).NotEmpty();
        RuleFor(oneonone => oneonone.LedId).NotEmpty();
    }
}

public class OneononeUpdateValidator : AbstractValidator<OneononeUpdate>
{
    public OneononeUpdateValidator()
    {
        When(oneonone => oneonone.Frequency is null, () => RuleFor(oneonone => oneonone.Frequency).NotEmpty())
            .Otherwise(() => RuleFor(oneonone => oneonone.Frequency).IsInEnum());
    }
}
