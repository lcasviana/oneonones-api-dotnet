using FluentValidation.Results;
using Oneonones.Services.Exceptions.Base;

namespace Oneonones.Services.Exceptions;

public class InvalidException : DomainException
{
    public InvalidException(IEnumerable<ValidationFailure> validationFailures, Exception? inner = null)
        : base(DomainExceptionType.Invalid, validationFailures.Select(failure => failure.ErrorMessage), inner) { }
}
