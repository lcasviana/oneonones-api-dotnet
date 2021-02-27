using System;

namespace Oneonones.Domain.Entities
{
    public class OneononeStatusEntity
    {
        public DateTime LastOccurrence { get; set; }
        public DateTime NextOccurrence { get; set; }
        public bool IsLate { get; set; }
    }
}