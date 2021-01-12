using Oneonones.Domain.Enums;
using System;

namespace Oneonones.Domain.Entities
{
    public class OneononeEntity
    {
        public EmployeeEntity Leader { get; set; }
        public EmployeeEntity Led { get; set; }
        public OneononeFrequencyEnum Frequency { get; set; }
        public DateTime? LastOneonone { get; set; }
    }
}