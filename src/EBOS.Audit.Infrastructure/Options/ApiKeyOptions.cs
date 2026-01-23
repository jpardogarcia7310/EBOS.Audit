namespace EBOS.Audit.Infrastructure.Options;

public sealed class ApiKeyOptions
{
    public Dictionary<string, string> Keys { get; set; } = new();
}