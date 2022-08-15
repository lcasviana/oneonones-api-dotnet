using Oneonones.Domain.Entities;

namespace Oneonones.Domain.Outputs;

public class MeetingOutput
{
    /// <summary>
    /// Meeting's id
    /// </summary>
    /// <example>
    /// 9d0e826c-497d-44f3-ac88-b9f6bcb9667d
    /// </example>
    public Guid Id { get; set; }
    /// <summary>
    /// One-on-one's leader
    /// </summary>
    public EmployeeOutput Leader { get; set; } = null!;
    /// <summary>
    /// One-on-one's led
    /// </summary>
    public EmployeeOutput Led { get; set; } = null!;
    /// <summary>
    /// Meeting's date
    /// </summary>
    /// <example>
    /// 2022-01-01
    /// </example>
    public DateTime MeetingDate { get; set; }
    /// <summary>
    /// Meeting's annotation
    /// </summary>
    /// <example>
    /// :)
    /// </example>
    public string? Annotation { get; set; }


    public static explicit operator MeetingOutput(Meeting meeting)
        => new()
        {
            Id = meeting.Id,
            Leader = (EmployeeOutput) meeting.Leader,
            Led = (EmployeeOutput) meeting.Led,
            MeetingDate = meeting.MeetingDate,
            Annotation = meeting.Annotation,
        };
}
