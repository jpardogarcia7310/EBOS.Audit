using System.Net.Http.Json;
using EBOS.Audit.Client.Contracts;

namespace EBOS.Audit.Client;

public sealed class AuditClient(HttpClient http) : IAuditClient
{
    public Task RegisterActivityAsync(ActivityLogRequest request, CancellationToken ct = default) =>
        http.PostAsJsonAsync("api/v1/audit/activity", request, ct);

    public Task RegisterChangeAsync(AuditChangeRequest request, CancellationToken ct = default) =>
        http.PostAsJsonAsync("api/v1/audit/changes", request, ct);

    public Task RegisterEventAsync(DomainEventRequest request, CancellationToken ct = default) =>
        http.PostAsJsonAsync("api/v1/audit/events", request, ct);
}