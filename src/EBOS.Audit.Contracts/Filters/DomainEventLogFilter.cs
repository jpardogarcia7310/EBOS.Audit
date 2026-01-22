namespace EBOS.Audit.Contracts.Filters;

public sealed record DomainEventLogFilter(string? SystemName, string? EventType, string? EntityName, string? EntityId,
    DateTime? From, DateTime? To, string? CorrelationId, int Page = 1, int PageSize = 50);