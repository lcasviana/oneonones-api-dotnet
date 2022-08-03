using Oneonones.Domain.Entities;
using Oneonones.Domain.Enumerations;

namespace Oneonones.Domain.Outputs;

public class OneononeOutput
{
    /// <summary>
    /// Oneonone's id
    /// </summary>
    /// <example>
    /// 10fcf429-6956-4de2-8602-0a068d401796
    /// </example>
    public Guid Id { get; set; }
    public EmployeeOutput Leader { get; set; } = null!;
    public EmployeeOutput Led { get; set; } = null!;
    /// <summary>
    /// Oneonone's frequency
    /// </summary>
    public Frequency Frequency { get; set; }


    public static explicit operator OneononeOutput(Oneonone oneonone)
        => new()
        {
            Id = oneonone.Id,
            Leader = (EmployeeOutput)oneonone.Leader,
            Led = (EmployeeOutput)oneonone.Led,
            Frequency = oneonone.Frequency,
        };
}
