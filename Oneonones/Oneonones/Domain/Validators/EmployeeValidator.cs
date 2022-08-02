using FluentValidation;
using Oneonones.Domain.Inputs;

namespace Oneonones.Domain.Validators;

public class EmployeeValidator : AbstractValidator<EmployeeInput>
{
    public EmployeeValidator()
    {
        RuleFor(employee => employee.Email).EmailAddress().MaximumLength(255);
        RuleFor(employee => employee.Name).NotEmpty().Length(3, 255);
    }
}
