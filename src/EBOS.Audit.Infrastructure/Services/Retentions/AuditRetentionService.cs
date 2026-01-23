using EBOS.Audit.Infrastructure.Options;
using EBOS.Audit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EBOS.Audit.Infrastructure.Services.Retentions;

public sealed class AuditRetentionService(AuditDbContext db, IOptions<AuditRetentionOptions> options)
{
    private readonly AuditRetentionOptions _options = options.Value;

    public async Task RunRetentionAsync(CancellationToken ct)
    {
        if (!_options.Enabled)
            return;

        var now = DateTime.UtcNow;

        await DeleteOldActivityLogsAsync(now, ct);
        await DeleteOldAuditChangesAsync(now, ct);
        await DeleteOldDomainEventsAsync(now, ct);
    }

    private async Task DeleteOldActivityLogsAsync(DateTime now, CancellationToken ct)
    {
        var limit = now.AddDays(-_options.ActivityDays);

        while (true)
        {
            var batch = await db.ActivityLogs
                .Where(x => x.Timestamp < limit)
                .OrderBy(x => x.Timestamp)
                .Take(_options.BatchSize)
                .ToListAsync(ct);

            if (batch.Count == 0)
                break;

            db.ActivityLogs.RemoveRange(batch);
            await db.SaveChangesAsync(ct);
        }
    }

    private async Task DeleteOldAuditChangesAsync(DateTime now, CancellationToken ct)
    {
        var limit = now.AddDays(-_options.ChangeDays);

        while (true)
        {
            var batch = await db.AuditChanges
                .Where(x => x.ChangedAt < limit)
                .OrderBy(x => x.ChangedAt)
                .Take(_options.BatchSize)
                .ToListAsync(ct);

            if (batch.Count == 0)
                break;

            db.AuditChanges.RemoveRange(batch);
            await db.SaveChangesAsync(ct);
        }
    }

    private async Task DeleteOldDomainEventsAsync(DateTime now, CancellationToken ct)
    {
        var limit = now.AddDays(-_options.EventDays);

        while (true)
        {
            var batch = await db.DomainEventLogs
                .Where(x => x.OccurredAt < limit)
                .OrderBy(x => x.OccurredAt)
                .Take(_options.BatchSize)
                .ToListAsync(ct);

            if (batch.Count == 0)
                break;

            db.DomainEventLogs.RemoveRange(batch);
            await db.SaveChangesAsync(ct);
        }
    }
}
