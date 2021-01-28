using Oneonones.Domain.Enums;

namespace Oneonones.Infrastructure.ViewModel
{
    public class OneononeInputModel
    {
        public string LeaderEmail { get; set; }
        public string LedEmail { get; set; }
        public OneononeFrequencyEnum Frequency { get; set; }
    }
}