using Oneonones.Domain.Enums;
using System;

namespace Oneonones.Infrastructure.ViewModel
{
    public class OneononeViewModel
    {
        public EmployeeViewModel Leader { get; set; }
        public EmployeeViewModel Led { get; set; }
        public OneononeFrequencyEnum Frequency { get; set; }
        public DateTime? LastOneonone { get; set; }
    }
}