namespace EBOS.Audit.Client.Contracts;

public sealed class DomainEventRequest(string systemName, string eventType, string entityName, string entityId,
    string payloadJson, DateTime occurredAt, string triggeredBy, string? correlationId)
{
    public string SystemName { get; } = systemName;
    public string EventType { get; } = eventType;
    public string EntityName { get; } = entityName;
    public string EntityId { get; } = entityId;
    public string PayloadJson { get; } = payloadJson;
    public DateTime OccurredAt { get; } = occurredAt;
    public string TriggeredBy { get; } = triggeredBy;
    public string? CorrelationId { get; } = correlationId;
}