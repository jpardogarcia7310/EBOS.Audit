using EBOS.Audit.Client.Contracts;
using EBOS.Audit.Domain.Entities;
using EBOS.Audit.Domain.Interfaces.Repositories;

namespace EBOS.Audit.Application.Services;

public sealed class AuditAppService(IAuditChangeRepository changes, IDomainEventLogRepository events,
    IActivityLogRepository activities)
{
    public async Task RegisterChangeAsync(AuditChangeRequest request, CancellationToken ct)
    {
        var change = AuditChange.Create(request.SystemName, request.EntityName, request.EntityId, request.PropertyName,
            request.OldValue, request.NewValue, request.ChangedAt, request.ChangedBy, request.CorrelationId
        );

        await changes.AddAsync(change, ct);
    }

    public async Task RegisterEventAsync(DomainEventRequest request, CancellationToken ct)
    {
        var evt = DomainEventLog.Create(request.SystemName, request.EventType, request.EntityName, request.EntityId,
            request.PayloadJson, request.OccurredAt, request.TriggeredBy, request.CorrelationId);

        await events.AddAsync(evt, ct);
    }

    public async Task RegisterActivityAsync(ActivityLogRequest request, CancellationToken ct)
    {
        var activity = ActivityLog.Create(request.SystemName, request.Action, request.Description, request.User,
            request.Timestamp, request.IpAddress, request.UserAgent, request.MetadataJson, request.CorrelationId
        );

        await activities.AddAsync(activity, ct);
    }
}
