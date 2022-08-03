using Microsoft.EntityFrameworkCore;
using Oneonones.Domain.Entities;

namespace Oneonones.Repositories.Context;

public class OneononeContext : DbContext
{
    public DbSet<Employee> Employee { get; set; } = null!;
    public DbSet<Oneonone> Oneonone { get; set; } = null!;
    public DbSet<Meeting> Meeting { get; set; } = null!;

    public OneononeContext(DbContextOptions<OneononeContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasIndex(employee => new { employee.Email }, "IX_employee_email")
            .IsUnique(true);

        modelBuilder.Entity<Oneonone>()
            .HasIndex(oneonone => new { oneonone.LeaderId, oneonone.LedId }, "IX_oneonone_leader")
            .IsUnique(true);

        modelBuilder.Entity<Oneonone>()
            .HasIndex(oneonone => new { oneonone.LedId, oneonone.LeaderId }, "IX_oneonone_led")
            .IsUnique(true);

        modelBuilder.Entity<Meeting>()
            .HasIndex(meeting => new { meeting.LeaderId, meeting.LedId, meeting.MeetingDate }, "IX_meeting_leader")
            .IsUnique(true);

        modelBuilder.Entity<Meeting>()
            .HasIndex(meeting => new { meeting.LedId, meeting.LeaderId, meeting.MeetingDate }, "IX_meeting_led")
            .IsUnique(true);
    }
}
