using System;

namespace Oneonones.Domain.Entities
{
    public class HistoricalEntity
    {
        public string Id { get; set; }
        public EmployeeEntity Leader { get; set; }
        public EmployeeEntity Led { get; set; }
        public DateTime Occurrence { get; set; }
        public string Commentary { get; set; }

        public HistoricalEntity() { }

        public HistoricalEntity(EmployeeEntity leader, EmployeeEntity led, DateTime occurrence, string commentary)
        {
            Id = Guid.NewGuid().ToString("D");
            Leader = leader;
            Led = led;
            Occurrence = occurrence;
            Commentary = commentary;
        }
    }
}