using EBOS.Audit.Contracts.Aggregates;
using EBOS.Audit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EBOS.Audit.Infrastructure.Services.Queries;

public sealed class AuditAggregationService(AuditDbContext db)
{
    // 1. Activity by system
    public async Task<IReadOnlyList<CountByStringResponse>> GetActivityBySystemAsync(CancellationToken ct)
        => await db.ActivityLogs
            .GroupBy(x => x.SystemName)
            .Select(g => new CountByStringResponse(g.Key, g.Count()))
            .OrderByDescending(x => x.Count)
            .ToListAsync(ct);

    // 2. Activity by user
    public async Task<IReadOnlyList<CountByStringResponse>> GetActivityByUserAsync(CancellationToken ct)
        => await db.ActivityLogs
            .GroupBy(x => x.User)
            .Select(g => new CountByStringResponse(g.Key, g.Count()))
            .OrderByDescending(x => x.Count)
            .ToListAsync(ct);

    // 3. Activity by action
    public async Task<IReadOnlyList<CountByStringResponse>> GetActivityByActionAsync(CancellationToken ct)
        => await db.ActivityLogs
            .GroupBy(x => x.Action)
            .Select(g => new CountByStringResponse(g.Key, g.Count()))
            .OrderByDescending(x => x.Count)
            .ToListAsync(ct);

    // 4. Activity per day
    public async Task<IReadOnlyList<CountByDateResponse>> GetActivityByDayAsync(CancellationToken ct)
        => await db.ActivityLogs
            .GroupBy(x => x.Timestamp.Date)
            .Select(g => new CountByDateResponse(g.Key, g.Count()))
            .OrderBy(x => x.Date)
            .ToListAsync(ct);

    // 5. Changes by entity
    public async Task<IReadOnlyList<CountByStringResponse>> GetChangesByEntityAsync(CancellationToken ct)
        => await db.AuditChanges
            .GroupBy(x => x.EntityName)
            .Select(g => new CountByStringResponse(g.Key, g.Count()))
            .OrderByDescending(x => x.Count)
            .ToListAsync(ct);

    // 6. Changes due to property
    public async Task<IReadOnlyList<CountByStringResponse>> GetChangesByPropertyAsync(CancellationToken ct)
        => await db.AuditChanges
            .GroupBy(x => x.PropertyName)
            .Select(g => new CountByStringResponse(g.Key, g.Count()))
            .OrderByDescending(x => x.Count)
            .ToListAsync(ct);

    // 7. Changes per user
    public async Task<IReadOnlyList<CountByStringResponse>> GetChangesByUserAsync(CancellationToken ct)
        => await db.AuditChanges
            .GroupBy(x => x.ChangedBy)
            .Select(g => new CountByStringResponse(g.Key, g.Count()))
            .OrderByDescending(x => x.Count)
            .ToListAsync(ct);

    // 8. Changes per day
    public async Task<IReadOnlyList<CountByDateResponse>> GetChangesByDayAsync(CancellationToken ct)
        => await db.AuditChanges
            .GroupBy(x => x.ChangedAt.Date)
            .Select(g => new CountByDateResponse(g.Key, g.Count()))
            .OrderBy(x => x.Date)
            .ToListAsync(ct);

    // 9. Events by type
    public async Task<IReadOnlyList<CountByStringResponse>> GetEventsByTypeAsync(CancellationToken ct)
        => await db.DomainEventLogs
            .GroupBy(x => x.EventType)
            .Select(g => new CountByStringResponse(g.Key, g.Count()))
            .OrderByDescending(x => x.Count)
            .ToListAsync(ct);

    // 10. Events by system
    public async Task<IReadOnlyList<CountByStringResponse>> GetEventsBySystemAsync(CancellationToken ct)
        => await db.DomainEventLogs
            .GroupBy(x => x.SystemName)
            .Select(g => new CountByStringResponse(g.Key, g.Count()))
            .OrderByDescending(x => x.Count)
            .ToListAsync(ct);

    // 11. Events by entity
    public async Task<IReadOnlyList<CountByStringResponse>> GetEventsByEntityAsync(CancellationToken ct)
        => await db.DomainEventLogs
            .GroupBy(x => x.EntityName)
            .Select(g => new CountByStringResponse(g.Key, g.Count()))
            .OrderByDescending(x => x.Count)
            .ToListAsync(ct);

    // 12. Events per day
    public async Task<IReadOnlyList<CountByDateResponse>> GetEventsByDayAsync(CancellationToken ct)
        => await db.DomainEventLogs
            .GroupBy(x => x.OccurredAt.Date)
            .Select(g => new CountByDateResponse(g.Key, g.Count()))
            .OrderBy(x => x.Date)
            .ToListAsync(ct);
}
