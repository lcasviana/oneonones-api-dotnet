using System;

namespace Oneonones.Infrastructure.ViewModels
{
    public class StatusViewModel
    {
        public DateTime LastOccurrence { get; set; }
        public DateTime NextOccurrence { get; set; }
        public bool IsLate { get; set; }
    }
}