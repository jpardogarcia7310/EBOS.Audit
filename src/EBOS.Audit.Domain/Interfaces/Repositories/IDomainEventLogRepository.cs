using EBOS.Audit.Domain.Entities;

namespace EBOS.Audit.Domain.Interfaces.Repositories;

public interface IDomainEventLogRepository
{
    Task AddAsync(DomainEventLog domainEvent, CancellationToken cancellationToken = default);
}