using EBOS.Core.Primitives;

namespace EBOS.Audit.Domain.Entities;

public sealed class ActivityLog : BaseEntity
{
    private ActivityLog(string systemName, string action, string description, string user, DateTime timestamp,
        string? ipAddress, string? userAgent, string? metadataJson, string? correlationId)
    {
        SystemName = systemName;
        Action = action;
        Description = description;
        User = user;
        Timestamp = timestamp;
        IpAddress = ipAddress;
        UserAgent = userAgent;
        MetadataJson = metadataJson;
        CorrelationId = correlationId;
    }

    public static ActivityLog Create(string systemName, string action, string description, string user,
        DateTime timestamp, string? ipAddress = null, string? userAgent = null, string? metadataJson = null,
        string? correlationId = null)
        => new(systemName, action, description, user, timestamp, ipAddress, userAgent, metadataJson, correlationId);

    public string SystemName { get; }
    public string Action { get; }
    public string Description { get; }
    public string User { get; }
    public DateTime Timestamp { get; }
    public string? IpAddress { get; }
    public string? UserAgent { get; }
    public string? MetadataJson { get; }
    public string? CorrelationId { get; }
}