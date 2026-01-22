using EBOS.Audit.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EBOS.Audit.Infrastructure.Persistence;

public sealed class AuditDbContext(DbContextOptions<AuditDbContext> options) : DbContext(options)
{
    public DbSet<AuditChange> AuditChanges => Set<AuditChange>();
    public DbSet<DomainEventLog> DomainEventLogs => Set<DomainEventLog>();
    public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuditDbContext).Assembly);
    }
}