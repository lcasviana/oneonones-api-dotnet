namespace Oneonones.Domain.Entities
{
    public class StatusEntity
    {
        public DateTime LastOccurrence { get; set; }
        public DateTime NextOccurrence { get; set; }
        public bool IsLate { get; set; }
    }
}