using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oneonones.Domain.Entities.Base;
using Oneonones.Domain.Enumerations;

namespace Oneonones.Domain.Entities;

[Table("oneonone")]
public class Oneonone : Entity
{
    [Column("leader_id"), Required]
    public Guid LeaderId { get; set; }

    [Column("led_id"), Required]
    public Guid LedId { get; set; }

    [Column("frequency_type"), Required]
    public Frequency Frequency { get; set; }


    [ForeignKey("LeaderId")]
    public virtual Employee Leader { get; set; } = null!;

    [ForeignKey("LedId")]
    public virtual Employee Led { get; set; } = null!;
}
