using Oneonones.Domain.Enumerations;

namespace Oneonones.Domain.Inputs;

public class OneononeInput
{
    public Guid? LeaderId { get; set; }
    public Guid? LedId { get; set; }
    public Frequency? Frequency { get; set; }
}
