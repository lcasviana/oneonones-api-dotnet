namespace Oneonones.Persistence.Models
{
    public class HistoricalModel
    {
        public string Id { get; set; }
        public string LeaderId { get; set; }
        public string LedId { get; set; }
        public DateTime Occurrence { get; set; }
        public string Commentary { get; set; }
    }
}