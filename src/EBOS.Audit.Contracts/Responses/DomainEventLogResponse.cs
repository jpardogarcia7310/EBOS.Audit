namespace EBOS.Audit.Contracts.Responses;

public sealed record DomainEventLogResponse(long Id, string SystemName, string EventType, string EntityName,
    string EntityId, string PayloadJson, DateTime OccurredAt, string TriggeredBy, string? CorrelationId);