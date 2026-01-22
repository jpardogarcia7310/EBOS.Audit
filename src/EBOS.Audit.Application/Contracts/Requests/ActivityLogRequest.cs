namespace EBOS.Audit.Application.Contracts.Requests;

public sealed class ActivityLogRequest
{
    public string SystemName { get; init; } = null!;     // "CRM", "SUP", etc.
    public string Action { get; init; } = null!;         // "Login", "CreateCustomer"
    public string Description { get; init; } = null!;    // texto legible

    public string User { get; init; } = null!;           // usuario que realizó la acción
    public DateTime Timestamp { get; init; }             // cuándo ocurrió

    public string? IpAddress { get; init; }              // opcional
    public string? UserAgent { get; init; }              // opcional
    public string? MetadataJson { get; init; }           // datos adicionales
    public string? CorrelationId { get; init; }          // para agrupar acciones
}