using System.Collections.Generic;

namespace Oneonones.Domain.Entities
{
    public class OneononeComposeEntity
    {
        public OneononeEntity Oneonone { get; set; }
        public IList<OneononeHistoricalEntity> Historical { get; set; }
        public OneononeStatusEntity Status { get; set; }
    }
}