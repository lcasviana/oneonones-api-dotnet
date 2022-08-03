using Oneonones.Domain.Entities;
using Oneonones.Domain.Enumerations;

namespace Oneonones.Domain.Outputs;

public class OneononeOutput
{
    public Guid Id { get; set; }
    public EmployeeOutput Leader { get; set; } = null!;
    public EmployeeOutput Led { get; set; } = null!;
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
