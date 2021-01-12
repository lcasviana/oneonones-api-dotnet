using System;

namespace Oneonones.Domain.Entities
{
    public class OneononeHistoricalEntity
    {
        public EmployeeEntity Leader { get; set; }
        public EmployeeEntity Led { get; set; }
        public DateTime Occurrence { get; set; }
        public string Commentary { get; set; }
    }
}