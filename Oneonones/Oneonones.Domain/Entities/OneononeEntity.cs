using Oneonones.Domain.Enums;

namespace Oneonones.Domain.Entities
{
    public class OneononeEntity
    {
        public EmployeeEntity Leader { get; set; }
        public EmployeeEntity Led { get; set; }
        public OneononeFrequencyEnum Frequency { get; set; }
    }
}