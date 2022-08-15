using FluentValidation;
using Oneonones.Domain.Inputs;

namespace Oneonones.Domain.Validators;

public class OneononeInsertValidator : AbstractValidator<OneononeInsert>
{
    public OneononeInsertValidator()
    {
        RuleFor(oneonone => oneonone.LeaderId)
            .NotEmpty();

        RuleFor(oneonone => oneonone.LedId)
            .NotEmpty();

        RuleFor(oneonone => oneonone.Frequency)
            .NotEmpty()
            .DependentRules(() =>
            {
                RuleFor(oneonone => oneonone.Frequency)
                    .IsInEnum();
            });
    }
}

public class OneononeUpdateValidator : AbstractValidator<OneononeUpdate>
{
    public OneononeUpdateValidator()
    {
        RuleFor(oneonone => oneonone.Frequency)
            .NotEmpty()
            .DependentRules(() =>
            {
                RuleFor(oneonone => oneonone.Frequency)
                    .IsInEnum();
            });
    }
}
