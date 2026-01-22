namespace EBOS.Audit.Contracts.Responses;

public sealed record ActivityLogResponse(long Id, string SystemName, string Action, string Description, string User,
    DateTime Timestamp, string? IpAddress, string? UserAgent, string? MetadataJson, string? CorrelationId);