using Meetings.Domain.Enums;
using System;

namespace Meetings.Infrastructure.ViewModel
{
    public class MeetingModel
    {
        public EmployeeModel Leader { get; set; }
        public EmployeeModel Led { get; set; }
        public MeetingFrequencyEnum Frequency { get; set; }
        public DateTime? LastMeeting { get; set; }
    }
}