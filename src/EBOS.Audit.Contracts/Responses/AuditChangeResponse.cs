namespace EBOS.Audit.Contracts.Responses;

public sealed record AuditChangeResponse(long Id, string SystemName, string EntityName, string EntityId,
    string PropertyName, string? OldValue, string? NewValue, DateTime ChangedAt, string ChangedBy,
    string? CorrelationId);