namespace Oneonones.Domain.Entities
{
    public class OneononeEntity
    {
        public string Id { get; set; }
        public EmployeeEntity Leader { get; set; }
        public EmployeeEntity Led { get; set; }
        public Frequency Frequency { get; set; }

        public OneononeEntity() { }

        public OneononeEntity(EmployeeEntity leader, EmployeeEntity led, Frequency frequency)
        {
            Id = Guid.NewGuid().ToString("D");
            Leader = leader;
            Led = led;
            Frequency = frequency;
        }
    }
}