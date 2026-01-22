namespace EBOS.Audit.Contracts.Filters;

public sealed record ActivityLogFilter(string? SystemName, string? User, DateTime? From, DateTime? To,
    string? CorrelationId, int Page = 1, int PageSize = 50);