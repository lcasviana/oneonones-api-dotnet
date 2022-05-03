namespace Oneonones.Infrastructure.ViewModels
{
    public class HistoricalViewModel
    {
        public string Id { get; set; }
        public EmployeeViewModel Leader { get; set; }
        public EmployeeViewModel Led { get; set; }
        public DateTime Occurrence { get; set; }
        public string Commentary { get; set; }
    }
}