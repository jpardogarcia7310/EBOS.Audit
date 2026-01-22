using EBOS.Audit.Contracts.Filters;
using EBOS.Audit.Contracts.Responses;
using EBOS.Audit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EBOS.Audit.Application.Services.Queries;

public sealed class AuditQueryService(AuditDbContext db)
{
    public async Task<PagedResult<ActivityLogResponse>> GetActivityLogsAsync(ActivityLogFilter filter, 
        CancellationToken ct)
    {
        var query = db.ActivityLogs.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.SystemName))
            query = query.Where(x => x.SystemName == filter.SystemName);
        if (!string.IsNullOrWhiteSpace(filter.User))
            query = query.Where(x => x.User == filter.User);
        if (filter.From.HasValue)
            query = query.Where(x => x.Timestamp >= filter.From);
        if (filter.To.HasValue)
            query = query.Where(x => x.Timestamp <= filter.To);
        if (!string.IsNullOrWhiteSpace(filter.CorrelationId))
            query = query.Where(x => x.CorrelationId == filter.CorrelationId);

        var total = await query.CountAsync(ct);
        var items = await query
            .OrderByDescending(x => x.Timestamp)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(x => new ActivityLogResponse(
                x.Id, x.SystemName, x.Action, x.Description, x.User,
                x.Timestamp, x.IpAddress, x.UserAgent, x.MetadataJson, x.CorrelationId))
            .ToListAsync(ct);

        return new PagedResult<ActivityLogResponse>(items, filter.Page, filter.PageSize, total);
    }

    public async Task<PagedResult<AuditChangeResponse>> GetAuditChangesAsync(AuditChangeFilter filter,
        CancellationToken ct)
    {
        var query = db.AuditChanges.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.SystemName))
            query = query.Where(x => x.SystemName == filter.SystemName);
        if (!string.IsNullOrWhiteSpace(filter.EntityName))
            query = query.Where(x => x.EntityName == filter.EntityName);
        if (!string.IsNullOrWhiteSpace(filter.EntityId))
            query = query.Where(x => x.EntityId == filter.EntityId);
        if (filter.From.HasValue)
            query = query.Where(x => x.ChangedAt >= filter.From);
        if (filter.To.HasValue)
            query = query.Where(x => x.ChangedAt <= filter.To);
        if (!string.IsNullOrWhiteSpace(filter.CorrelationId))
            query = query.Where(x => x.CorrelationId == filter.CorrelationId);

        var total = await query.CountAsync(ct);
        var items = await query
            .OrderByDescending(x => x.ChangedAt)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(x => new AuditChangeResponse(
                x.Id, x.SystemName, x.EntityName, x.EntityId, x.PropertyName,
                x.OldValue, x.NewValue, x.ChangedAt, x.ChangedBy, x.CorrelationId))
            .ToListAsync(ct);

        return new PagedResult<AuditChangeResponse>(items, filter.Page, filter.PageSize, total);
    }

    public async Task<PagedResult<DomainEventLogResponse>> GetDomainEventsAsync(DomainEventLogFilter filter,
        CancellationToken ct)
    {
        var query = db.DomainEventLogs.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.SystemName))
            query = query.Where(x => x.SystemName == filter.SystemName);
        if (!string.IsNullOrWhiteSpace(filter.EventType))
            query = query.Where(x => x.EventType == filter.EventType);
        if (!string.IsNullOrWhiteSpace(filter.EntityName))
            query = query.Where(x => x.EntityName == filter.EntityName);
        if (!string.IsNullOrWhiteSpace(filter.EntityId))
            query = query.Where(x => x.EntityId == filter.EntityId);
        if (filter.From.HasValue)
            query = query.Where(x => x.OccurredAt >= filter.From);
        if (filter.To.HasValue)
            query = query.Where(x => x.OccurredAt <= filter.To);
        if (!string.IsNullOrWhiteSpace(filter.CorrelationId))
            query = query.Where(x => x.CorrelationId == filter.CorrelationId);

        var total = await query.CountAsync(ct);

        var items = await query
            .OrderByDescending(x => x.OccurredAt)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(x => new DomainEventLogResponse(
                x.Id, x.SystemName, x.EventType, x.EntityName, x.EntityId,
                x.PayloadJson, x.OccurredAt, x.TriggeredBy, x.CorrelationId))
            .ToListAsync(ct);

        return new PagedResult<DomainEventLogResponse>(items, filter.Page, filter.PageSize, total);
    }
}
