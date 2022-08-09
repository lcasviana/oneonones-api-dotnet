using System.ComponentModel;
using EnumsNET;
using Oneonones.Services.Exceptions.Base;

namespace Oneonones.Services.Exceptions;

public enum NotFoundEntity
{
    [Description("Employee")]
    Employee,

    [Description("Meeting")]
    Meeting,

    [Description("One-on-one")]
    Oneonone,
}

public class NotFoundException : DomainException
{
    private static string NotFoundMessage(NotFoundEntity entity) => $"{entity.AsString(EnumFormat.Description)} not found.";

    public NotFoundException(NotFoundEntity entity, Exception? inner = null)
        : base(DomainExceptionType.NotFound, NotFoundMessage(entity), inner) { }
}
