using Oneonones.Domain.Enums;

namespace Oneonones.Infrastructure.ViewModel
{
    public class OneononeUpdateModel
    {
        public string Leader { get; set; }
        public string Led { get; set; }
        public OneononeFrequencyEnum Frequency { get; set; }
    }
}