using System;

namespace Oneonones.Infrastructure.ViewModels
{
    public class OneononeHistoricalInputViewModel
    {
        public string LeaderEmail { get; set; }
        public string LedEmail { get; set; }
        public DateTime Occurrence { get; set; }
        public string Commentary { get; set; }
    }
}