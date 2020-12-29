using System;

namespace Meetings.Domain.Entities
{
    public class MeetingHistoricalEntity
    {
        public EmployeeEntity Leader { get; set; }
        public EmployeeEntity Led { get; set; }
        public DateTime Occurrence { get; set; }
        public string Commentary { get; set; }
    }
}