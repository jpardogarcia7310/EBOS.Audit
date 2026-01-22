namespace EBOS.Audit.Application.Contracts.Requests;

public sealed class AuditChangeRequest(string systemName, string entityName, string entityId, string propertyName,
    string? oldValue, string? newValue, DateTime changedAt, string changedBy, string? correlationId)
{
    public string SystemName { get; } = systemName;
    public string EntityName { get; } = entityName;
    public string EntityId { get; } = entityId;
    public string PropertyName { get; } = propertyName;
    public string? OldValue { get; } = oldValue;
    public string? NewValue { get; } = newValue;
    public DateTime ChangedAt { get; } = changedAt;
    public string ChangedBy { get; } = changedBy;
    public string? CorrelationId { get; } = correlationId;
}