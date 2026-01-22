using EBOS.Audit.Application.Contracts.Requests;
using EBOS.Audit.Domain.Entities;
using EBOS.Audit.Domain.Interfaces.Services;

namespace EBOS.Audit.Application.Services;

public sealed class AuditAppService(IAuditService auditService)
{
    public Task RegisterChangeAsync(AuditChangeRequest request, CancellationToken ct = default)
    {
        var change = new AuditChange(
            request.SystemName,
            request.EntityName,
            request.EntityId,
            request.PropertyName,
            request.OldValue,
            request.NewValue,
            request.ChangedAt,
            request.ChangedBy,
            request.CorrelationId);

        return auditService.RegisterChangeAsync(change, ct);
    }

    public Task RegisterEventAsync(DomainEventRequest request, CancellationToken ct = default)
    {
        var evt = new DomainEventLog(
            request.SystemName,
            request.EventType,
            request.EntityName,
            request.EntityId,
            request.PayloadJson,
            request.OccurredAt,
            request.TriggeredBy,
            request.CorrelationId);

        return auditService.RegisterEventAsync(evt, ct);
    }

    public Task RegisterActivityAsync(ActivityLogRequest request, CancellationToken ct = default)
    {
        var activity = new ActivityLog(
            request.SystemName,
            request.Action,
            request.Description,
            request.User,
            request.Timestamp,
            request.IpAddress,
            request.UserAgent,
            request.MetadataJson,
            request.CorrelationId);

        return auditService.RegisterActivityAsync(activity, ct);
    }
}