namespace EBOS.Audit.Client.Options;

public sealed class AuditClientOptions
{
    public string BaseUrl { get; set; } = null!;
    public string? ApiKey { get; set; }
}