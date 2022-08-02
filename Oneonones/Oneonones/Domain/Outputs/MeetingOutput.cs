using Oneonones.Domain.Entities;

namespace Oneonones.Domain.Outputs;

public class MeetingOutput
{
    public Guid Id { get; set; }
    public EmployeeOutput Leader { get; set; } = null!;
    public EmployeeOutput Led { get; set; } = null!;
    public DateTime MeetingDate { get; set; }
    public string? Annotation { get; set; }

    public static explicit operator MeetingOutput(Meeting meeting)
        => new()
        {
            Id = meeting.Id,
            Leader = (EmployeeOutput)meeting.Leader,
            Led = (EmployeeOutput)meeting.Led,
            MeetingDate = meeting.MeetingDate,
            Annotation = meeting.Annotation,
        };
}
