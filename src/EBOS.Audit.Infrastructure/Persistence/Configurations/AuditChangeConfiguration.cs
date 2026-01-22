using EBOS.Audit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EBOS.Audit.Infrastructure.Persistence.Configurations;

public sealed class AuditChangeConfiguration : IEntityTypeConfiguration<AuditChange>
{
    public void Configure(EntityTypeBuilder<AuditChange> builder)
    {
        builder.ToTable("AuditChanges", "Audit");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.SystemName).IsRequired().HasMaxLength(50);
        builder.Property(a => a.EntityName).IsRequired().HasMaxLength(100);
        builder.Property(a => a.EntityId).IsRequired().HasMaxLength(100);
        builder.Property(a => a.PropertyName).IsRequired().HasMaxLength(100);
        builder.Property(a => a.ChangedBy).IsRequired().HasMaxLength(100);
        builder.Property(a => a.CorrelationId).HasMaxLength(100);

        builder.HasIndex(a => new { a.SystemName, a.EntityName, a.EntityId })
            .HasDatabaseName("IX_AuditChange_Entity");

        builder.HasIndex(a => a.ChangedAt)
            .HasDatabaseName("IX_AuditChange_ChangedAt");
    }
}