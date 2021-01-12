using Oneonones.Domain.Enums;
using System;

namespace Oneonones.Infrastructure.ViewModel
{
    public class OneononeModel
    {
        public EmployeeModel Leader { get; set; }
        public EmployeeModel Led { get; set; }
        public OneononeFrequencyEnum Frequency { get; set; }
        public DateTime? LastOneonone { get; set; }
    }
}