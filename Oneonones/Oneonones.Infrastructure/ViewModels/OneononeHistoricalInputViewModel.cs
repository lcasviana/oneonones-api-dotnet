using System;

namespace Oneonones.Infrastructure.ViewModels
{
    public class OneononeHistoricalInputViewModel
    {
        public string LeaderId { get; set; }
        public string LedId { get; set; }
        public DateTime Occurrence { get; set; }
        public string Commentary { get; set; }
    }
}