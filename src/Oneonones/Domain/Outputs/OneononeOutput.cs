using System.Text.Json.Serialization;
using Oneonones.Domain.Entities;
using Oneonones.Domain.Enumerations;

namespace Oneonones.Domain.Outputs;

public class OneononeOutput
{
    /// <summary>
    /// One-on-one's id
    /// </summary>
    /// <example>
    /// 10fcf429-6956-4de2-8602-0a068d401796
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
    /// One-on-one's frequency
    /// </summary>
    public Frequency Frequency { get; set; }
    /// <summary>
    /// One-on-one's meetings
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<MeetingOutput>? Meetings { get; set; }
    /// <summary>
    /// One-on-one's status
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public StatusOutput? Status { get; set; }


    public static explicit operator OneononeOutput(Oneonone oneonone)
        => new()
        {
            Id = oneonone.Id,
            Leader = (EmployeeOutput) oneonone.Leader,
            Led = (EmployeeOutput) oneonone.Led,
            Frequency = oneonone.Frequency,
        };
}
