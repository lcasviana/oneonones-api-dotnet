using Oneonones.Services.Exceptions.Base;

namespace Oneonones.Services.Exceptions;

public class NotFoundException : DomainException
{
    public NotFoundException(string message, Exception? inner = null)
        : base(DomainExceptionType.NotFound, message, inner) { }
}
