using System;

namespace Meetings.Infrastructure.ViewModel
{
    public class MeetingHistoricalModel
    {
        public EmployeeModel Leader { get; set; }
        public EmployeeModel Led { get; set; }
        public DateTime Occurrence { get; set; }
        public string Commentary { get; set; }
    }
}