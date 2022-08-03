using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oneonones.Domain.Entities.Base;

namespace Oneonones.Domain.Entities;

[Table("meeting")]
public class Meeting : Entity
{
    [Column("leader_id"), Required]
    public Guid LeaderId { get; set; }

    [Column("led_id"), Required]
    public Guid LedId { get; set; }

    [Column("meeting_date"), Required]
    public DateTime MeetingDate { get; set; }

    [Column("annotation"), StringLength(2047)]
    public string? Annotation { get; set; }


    [ForeignKey("LeaderId")]
    public virtual Employee Leader { get; set; } = null!;

    [ForeignKey("LedId")]
    public virtual Employee Led { get; set; } = null!;


    public Meeting() { }

    public Meeting(Guid leaderId, Guid ledId, DateTime meetingDate, string? annotation)
    {
        LeaderId = leaderId;
        LedId = ledId;
        MeetingDate = meetingDate;
        Annotation = annotation?.Trim();
    }

    public void Update(DateTime meetingDate, string? annotation)
    {
        MeetingDate = meetingDate;
        Annotation = annotation?.Trim();
    }
}
