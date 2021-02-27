using Oneonones.Domain.Enums;

namespace Oneonones.Infrastructure.ViewModels
{
    public class OneononeViewModel
    {
        public EmployeeViewModel Leader { get; set; }
        public EmployeeViewModel Led { get; set; }
        public OneononeFrequencyEnum Frequency { get; set; }
    }
}