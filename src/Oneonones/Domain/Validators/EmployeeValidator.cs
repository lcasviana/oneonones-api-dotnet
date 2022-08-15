using FluentValidation;
using Oneonones.Domain.Inputs;

namespace Oneonones.Domain.Validators;

public class EmployeeValidator : AbstractValidator<EmployeeInput>
{
    public EmployeeValidator()
    {
        RuleFor(employee => employee.Email)
            .EmailAddress()
            .MaximumLength(255);

        RuleFor(employee => employee.Name)
            .NotEmpty()
            .DependentRules(() =>
            {
                RuleFor(employee => employee.Name)
                    .Length(3, 255)
                    .Matches("^[a-zA-Z]+( [a-zA-Z]+)*$");
            });
    }
}
