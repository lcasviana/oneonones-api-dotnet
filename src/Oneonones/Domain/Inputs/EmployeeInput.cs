namespace Oneonones.Domain.Inputs;

public class EmployeeInput
{
    /// <summary>
    /// Employee's email
    /// </summary>
    /// <example>
    /// prefix@domain.br
    /// </example>
    public string? Email { get; set; }
    /// <summary>
    /// Employee's name
    /// </summary>
    /// <example>
    /// First Middle Surname
    /// </example>
    public string? Name { get; set; }
}
