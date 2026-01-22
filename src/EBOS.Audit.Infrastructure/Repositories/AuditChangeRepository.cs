using EBOS.Audit.Domain.Entities;
using EBOS.Audit.Domain.Interfaces.Repositories;
using EBOS.Audit.Infrastructure.Persistence;

namespace EBOS.Audit.Infrastructure.Repositories;

public sealed class AuditChangeRepository(AuditDbContext context) : IAuditChangeRepository
{
    public async Task AddAsync(AuditChange change, CancellationToken cancellationToken = default)
    {
        await context.AuditChanges.AddAsync(change, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}