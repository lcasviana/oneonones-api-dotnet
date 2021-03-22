using Oneonones.Domain.Enums;

namespace Oneonones.Domain.Entities
{
    public class OneononeInputEntity
    {
        public string LeaderId { get; set; }
        public string LedId { get; set; }
        public FrequencyEnum Frequency { get; set; }
    }
}