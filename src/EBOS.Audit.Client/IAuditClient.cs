using EBOS.Audit.Client.Contracts;

namespace EBOS.Audit.Client;

public interface IAuditClient
{
    Task RegisterActivityAsync(ActivityLogRequest request, CancellationToken ct = default);
    Task RegisterChangeAsync(AuditChangeRequest request, CancellationToken ct = default);
    Task RegisterEventAsync(DomainEventRequest request, CancellationToken ct = default);
}