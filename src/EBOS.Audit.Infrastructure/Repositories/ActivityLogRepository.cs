using EBOS.Audit.Domain.Entities;
using EBOS.Audit.Domain.Interfaces.Repositories;
using EBOS.Audit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EBOS.Audit.Infrastructure.Repositories;

public sealed class ActivityLogRepository(AuditDbContext context) : IActivityLogRepository
{
    public async Task AddAsync(ActivityLog activity, CancellationToken cancellationToken = default)
    {
        await context.ActivityLogs.AddAsync(activity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}