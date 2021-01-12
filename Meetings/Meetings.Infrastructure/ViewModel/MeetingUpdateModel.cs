using Meetings.Domain.Enums;

namespace Meetings.Infrastructure.ViewModel
{
    public class MeetingUpdateModel
    {
        public string Leader { get; set; }
        public string Led { get; set; }
        public MeetingFrequencyEnum Frequency { get; set; }
    }
}