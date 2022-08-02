namespace Oneonones.Infrastructure.ViewModels
{
    public class OneononeViewModel
    {
        public string Id { get; set; }
        public EmployeeViewModel Leader { get; set; }
        public EmployeeViewModel Led { get; set; }
        public Frequency Frequency { get; set; }
    }
}