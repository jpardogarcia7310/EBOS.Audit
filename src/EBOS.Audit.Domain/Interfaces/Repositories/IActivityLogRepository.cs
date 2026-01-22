using EBOS.Audit.Domain.Entities;

namespace EBOS.Audit.Domain.Interfaces.Repositories;

public interface IActivityLogRepository
{
    Task AddAsync(ActivityLog activity, CancellationToken cancellationToken = default);
}