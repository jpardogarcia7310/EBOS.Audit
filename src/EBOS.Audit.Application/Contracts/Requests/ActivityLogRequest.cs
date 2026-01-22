namespace EBOS.Audit.Application.Contracts.Requests;

public sealed class ActivityLogRequest(string systemName, string action, string description, string user,
    DateTime timestamp, string? ipAddress, string? userAgent, string? metadataJson, string? correlationId)
{
    public string SystemName { get; } = systemName;
    public string Action { get; } = action;
    public string Description { get; } = description;
    public string User { get; } = user;
    public DateTime Timestamp { get; } = timestamp;
    public string? IpAddress { get; } = ipAddress;
    public string? UserAgent { get; } = userAgent;
    public string? MetadataJson { get; } = metadataJson;
    public string? CorrelationId { get; } = correlationId;
}