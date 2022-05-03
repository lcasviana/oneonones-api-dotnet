namespace Oneonones.Domain.Entities
{
    public class DashboardEntity
    {
        public EmployeeEntity Employee { get; set; }
        public IList<OneononeComposeEntity> Oneonones { get; set; }
    }
}