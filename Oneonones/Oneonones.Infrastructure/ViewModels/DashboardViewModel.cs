namespace Oneonones.Infrastructure.ViewModels
{
    public class DashboardViewModel
    {
        public EmployeeViewModel Employee { get; set; }
        public IList<OneononeComposeViewModel> Oneonones { get; set; }
    }
}