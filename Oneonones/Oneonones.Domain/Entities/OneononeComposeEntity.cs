namespace Oneonones.Domain.Entities
{
    public class OneononeComposeEntity
    {
        public OneononeEntity Oneonone { get; set; }
        public IList<HistoricalEntity> Historical { get; set; }
        public StatusEntity Status { get; set; }
    }
}