namespace Oneonones.Domain.Outputs;

public class DashboardOutput
{
    /// <summary>
    /// Employee
    /// </summary>
    public EmployeeOutput Employee { get; set; } = null!;
    /// <summary>
    /// One-on-ones
    /// </summary>
    public IEnumerable<OneononeOutput> Oneonones { get; set; } = null!;


    public DashboardOutput(EmployeeOutput employee, IEnumerable<OneononeOutput> oneonones)
    {
        Employee = employee;
        Oneonones = oneonones;
    }
}
