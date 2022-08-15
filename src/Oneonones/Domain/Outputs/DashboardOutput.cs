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
    public IList<OneononeOutput> Oneonones { get; set; } = null!;


    public DashboardOutput(EmployeeOutput employee, IList<OneononeOutput> oneonones)
    {
        Employee = employee;
        Oneonones = oneonones;
    }
}
