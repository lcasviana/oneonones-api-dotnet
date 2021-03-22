using System;

namespace Oneonones.Domain.Entities
{
    public class HistoricalInputEntity
    {
        public string LeaderId { get; set; }
        public string LedId { get; set; }
        public DateTime Occurrence { get; set; }
        public string Commentary { get; set; }
    }
}