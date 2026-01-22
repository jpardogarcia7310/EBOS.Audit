namespace EBOS.Audit.Contracts.Filters;

public sealed record AuditChangeFilter(string? SystemName, string? EntityName, string? EntityId, DateTime? From,
    DateTime? To, string? CorrelationId, int Page = 1, int PageSize = 50);