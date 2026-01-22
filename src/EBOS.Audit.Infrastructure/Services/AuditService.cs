using EBOS.Audit.Domain.Entities;
using EBOS.Audit.Domain.Interfaces.Repositories;
using EBOS.Audit.Domain.Interfaces.Services;

namespace EBOS.Audit.Infrastructure.Services;

public sealed class AuditService(IAuditChangeRepository changes, IDomainEventLogRepository events,
    IActivityLogRepository activities) : IAuditService
{

    public Task RegisterChangeAsync(AuditChange change, CancellationToken ct = default)
        => changes.AddAsync(change, ct);

    public Task RegisterEventAsync(DomainEventLog domainEvent, CancellationToken ct = default)
        => events.AddAsync(domainEvent, ct);

    public Task RegisterActivityAsync(ActivityLog activity, CancellationToken ct = default)
        => activities.AddAsync(activity, ct);
}