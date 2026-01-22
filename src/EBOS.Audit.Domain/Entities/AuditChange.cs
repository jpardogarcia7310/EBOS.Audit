using EBOS.Core.Primitives;

namespace EBOS.Audit.Domain.Entities;

public sealed class AuditChange: BaseEntity
{
    private AuditChange(string systemName, string entityName, string entityId, string propertyName, string? oldValue,
        string? newValue, DateTime changedAt, string changedBy, string? correlationId)
    {
        SystemName = systemName;
        EntityName = entityName;
        EntityId = entityId;
        PropertyName = propertyName;
        OldValue = oldValue;
        NewValue = newValue;
        ChangedAt = changedAt;
        ChangedBy = changedBy;
        CorrelationId = correlationId;
    }

    public static AuditChange Create(string systemName, string entityName, string entityId, string propertyName,
        string? oldValue, string? newValue, DateTime timestamp, string changedBy, string? correlationId = null)
        => new(systemName, entityName, entityId, propertyName, oldValue, newValue, timestamp, changedBy, correlationId);

    public string SystemName { get; }
    public string EntityName { get; }
    public string EntityId { get; }
    public string PropertyName { get; }
    public string? OldValue { get; }
    public string? NewValue { get; }
    public DateTime ChangedAt { get; }
    public string ChangedBy { get; }
    public string? CorrelationId { get; }
}