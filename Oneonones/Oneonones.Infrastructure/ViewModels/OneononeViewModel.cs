using Oneonones.Domain.Common;
using Oneonones.Domain.Enums;

namespace Oneonones.Infrastructure.ViewModels
{
    public class OneononeViewModel : Identifier
    {
        public EmployeeViewModel Leader { get; set; }
        public EmployeeViewModel Led { get; set; }
        public OneononeFrequencyEnum Frequency { get; set; }
    }
}