using EBOS.Audit.Domain.Entities;
using EBOS.Audit.Domain.Interfaces.Repositories;
using EBOS.Audit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EBOS.Audit.Infrastructure.Repositories;

public sealed class DomainEventLogRepository(AuditDbContext context) : IDomainEventLogRepository
{
    public async Task AddAsync(DomainEventLog domainEvent, CancellationToken cancellationToken = default)
    {
        await context.DomainEventLogs.AddAsync(domainEvent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}