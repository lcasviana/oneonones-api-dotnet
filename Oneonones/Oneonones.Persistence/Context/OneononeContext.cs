using Microsoft.EntityFrameworkCore;

namespace Oneonones.Persistence.Context;

public class OneononeContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<OneononeContext> Oneonones { get; set; }
}
