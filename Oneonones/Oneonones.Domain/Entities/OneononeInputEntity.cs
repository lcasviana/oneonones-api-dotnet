using Oneonones.Domain.Enums;

namespace Oneonones.Domain.Entities
{
    public class OneononeInputEntity
    {
        public string LeaderEmail { get; set; }
        public string LedEmail { get; set; }
        public OneononeFrequencyEnum Frequency { get; set; }
    }
}