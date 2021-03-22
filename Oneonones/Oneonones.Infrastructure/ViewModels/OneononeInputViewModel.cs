using Oneonones.Domain.Enums;

namespace Oneonones.Infrastructure.ViewModels
{
    public class OneononeInputViewModel
    {
        public string LeaderId { get; set; }
        public string LedId { get; set; }
        public FrequencyEnum Frequency { get; set; }
    }
}