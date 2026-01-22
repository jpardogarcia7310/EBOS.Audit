namespace EBOS.Audit.Application.Contracts.Requests;

public sealed class DomainEventRequest
{
    public string SystemName { get; init; } = null!;     // "CRM", "SUP", "WRH", "INV"
    public string EventType { get; init; } = null!;      // "CustomerCreated", "OrderShipped"
    public string EntityName { get; init; } = null!;     // "Customer", "Order"
    public string EntityId { get; init; } = null!;       // string para soportar long, GUID, etc.

    public string PayloadJson { get; init; } = null!;    // snapshot del evento
    public DateTime OccurredAt { get; init; }            // fecha del evento
    public string TriggeredBy { get; init; } = null!;    // usuario o sistema
    public string? CorrelationId { get; init; }          // para agrupar eventos
}