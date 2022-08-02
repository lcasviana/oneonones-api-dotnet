using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oneonones.Domain.Entities.Base;

public abstract class Entity
{
    [Column("id"), Required, Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}
