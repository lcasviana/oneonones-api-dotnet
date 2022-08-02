using Oneonones.Domain.Entities;

namespace Oneonones.Domain.Outputs;

public class EmployeeOutput
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;

    public static explicit operator EmployeeOutput(Employee employee)
        => new()
        {
            Id = employee.Id,
            Email = employee.Email,
            Name = employee.Name,
        };
}
