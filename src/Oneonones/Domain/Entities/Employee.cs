using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oneonones.Domain.Entities.Base;

namespace Oneonones.Domain.Entities;

[Table("employee")]
public class Employee : Entity
{
    [Column("email"), StringLength(255), Required]
    public string Email { get; set; } = null!;

    [Column("name"), StringLength(255), Required]
    public string Name { get; set; } = null!;


    public Employee() { }

    public Employee(string email, string name)
    {
        Email = email;
        Name = name.Trim();
    }

    public void Update(string email, string name)
    {
        Email = email;
        Name = name.Trim();
    }
}
