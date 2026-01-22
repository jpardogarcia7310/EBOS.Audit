using EBOS.Core.Primitives;

namespace EBOS.Audit.Domain.Entities;

public sealed class DomainEventLog : BaseEntity
{
    // EF Core constructor

    // Constructor privado para creación controlada
    private DomainEventLog(string systemName, string eventType, string entityName, string entityId, string payloadJson,
        DateTime occurredAt, string triggeredBy, string? correlationId)
    {
        SystemName = systemName;
        EventType = eventType;
        EntityName = entityName;
        EntityId = entityId;
        PayloadJson = payloadJson;
        OccurredAt = occurredAt;
        TriggeredBy = triggeredBy;
        CorrelationId = correlationId;
    }

    // Factory estática: punto único de creación
    public static DomainEventLog Create(string systemName, string eventType, string entityName, string entityId,
        string payloadJson, DateTime occurredAt, string triggeredBy, string? correlationId = null)
        => new(systemName, eventType, entityName, entityId, payloadJson, occurredAt, triggeredBy, correlationId);

    public string SystemName { get; }
    public string EventType { get; }
    public string EntityName { get; }
    public string EntityId { get; }
    public string PayloadJson { get; }
    public DateTime OccurredAt { get; }
    public string TriggeredBy { get; }
    public string? CorrelationId { get; }
}