using Meetings.Domain.Enums;
using System;

namespace Meetings.Domain.Entities
{
    public class MeetingEntity
    {
        public EmployeeEntity Leader { get; set; }
        public EmployeeEntity Led { get; set; }
        public MeetingFrequencyEnum Frequency { get; set; }
        public DateTime? LastMeeting { get; set; }
    }
}