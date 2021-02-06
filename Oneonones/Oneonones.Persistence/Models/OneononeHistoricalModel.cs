using System;

namespace Oneonones.Persistence.Models
{
    public class OneononeHistoricalModel
    {
        public string LeaderEmail { get; set; }
        public string LedEmail { get; set; }
        public DateTime Occurrence { get; set; }
        public string Commentary { get; set; }
    }
}