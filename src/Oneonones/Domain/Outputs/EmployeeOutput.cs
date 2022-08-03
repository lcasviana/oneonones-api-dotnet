using Oneonones.Domain.Entities;

namespace Oneonones.Domain.Outputs;

public class EmployeeOutput
{
    /// <summary>
    /// Employee's id
    /// </summary>
    /// <example>
    /// aa4cc653-6148-496c-b53f-171e4608e79c
    /// </example>
    public Guid Id { get; set; }
    /// <summary>
    /// Employee's email
    /// </summary>
    /// <example>
    /// prefix@domain.br
    /// </example>
    public string Email { get; set; } = null!;
    /// <summary>
    /// Employee's name
    /// </summary>
    /// <example>
    /// First Middle Surname
    /// </example>
    public string Name { get; set; } = null!;


    public static explicit operator EmployeeOutput(Employee employee)
        => new()
        {
            Id = employee.Id,
            Email = employee.Email,
            Name = employee.Name,
        };
}
