using System;

namespace Oneonones.Domain.Entities
{
    public class OneononeHistoricalInputEntity
    {
        public string LeaderEmail { get; set; }
        public string LedEmail { get; set; }
        public DateTime Occurrence { get; set; }
        public string Commentary { get; set; }
    }
}