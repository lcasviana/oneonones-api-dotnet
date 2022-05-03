namespace Oneonones.Domain.Entities
{
    public class EmployeeEntity
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public EmployeeEntity() { }

        public EmployeeEntity(string email, string name)
        {
            Id = Guid.NewGuid().ToString("D");
            Email = email;
            Name = name;
        }
    }
}