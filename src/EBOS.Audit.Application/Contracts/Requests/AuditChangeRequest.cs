namespace EBOS.Audit.Application.Contracts.Requests;

public sealed class AuditChangeRequest
{
    public string SystemName { get; init; } = null!;
    public string EntityName { get; init; } = null!;
    public string EntityId { get; init; } = null!;
    public string PropertyName { get; init; } = null!;
    public string? OldValue { get; init; }
    public string? NewValue { get; init; }
    public DateTime ChangedAt { get; init; }
    public string ChangedBy { get; init; } = null!;
    public string? CorrelationId { get; init; }
}