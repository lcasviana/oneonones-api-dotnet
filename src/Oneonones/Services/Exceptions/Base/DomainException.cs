namespace Oneonones.Services.Exceptions.Base;

public enum DomainExceptionType
{
    NotFound,
    Invalid,
}

public abstract class DomainException : Exception
{
    public DomainExceptionType Type { get; set; }
    public IEnumerable<string> Errors { get; set; }

    protected DomainException(DomainExceptionType type, IEnumerable<string> messages, Exception? inner = null)
        : base(string.Join(" ", messages), inner)
    {
        Type = type;
        Errors = messages;
    }

    protected DomainException(DomainExceptionType type, string message, Exception? inner = null)
        : base(message, inner)
    {
        Type = type;
        Errors = new[] { message };
    }
}
