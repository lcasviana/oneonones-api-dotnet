namespace Oneonones.Domain.Inputs;

public class MeetingInput
{
    public Guid? LeaderId { get; set; }
    public Guid? LedId { get; set; }
    public DateTime? MeetingDate { get; set; }
    public string? Annotation { get; set; }
}
