using Asp.Versioning;
using EBOS.Audit.Infrastructure.Services.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBOS.Audit.Api.Controllers.Aggregates;

[ApiController]
[ApiVersion(2.0)]
[Route("api/v{version:apiVersion}/audit/aggregates")]
public sealed class AuditAggregatesController(AuditAggregationService service) : ControllerBase
{
    [Authorize(Policy = "AuditRead")]
    [HttpGet("activity/by-system")]
    public async Task<IActionResult> ActivityBySystem(CancellationToken ct)
        => Ok(await service.GetActivityBySystemAsync(ct));

    [Authorize(Policy = "AuditRead")]
    [HttpGet("activity/by-user")]
    public async Task<IActionResult> ActivityByUser(CancellationToken ct)
        => Ok(await service.GetActivityByUserAsync(ct));

    [Authorize(Policy = "AuditRead")]
    [HttpGet("activity/by-action")]
    public async Task<IActionResult> ActivityByAction(CancellationToken ct)
        => Ok(await service.GetActivityByActionAsync(ct));

    [Authorize(Policy = "AuditRead")]
    [HttpGet("activity/by-day")]
    public async Task<IActionResult> ActivityByDay(CancellationToken ct)
        => Ok(await service.GetActivityByDayAsync(ct));

    [Authorize(Policy = "AuditRead")]
    [HttpGet("changes/by-entity")]
    public async Task<IActionResult> ChangesByEntity(CancellationToken ct)
        => Ok(await service.GetChangesByEntityAsync(ct));

    [Authorize(Policy = "AuditRead")]
    [HttpGet("changes/by-property")]
    public async Task<IActionResult> ChangesByProperty(CancellationToken ct)
        => Ok(await service.GetChangesByPropertyAsync(ct));

    [Authorize(Policy = "AuditRead")]
    [HttpGet("changes/by-user")]
    public async Task<IActionResult> ChangesByUser(CancellationToken ct)
        => Ok(await service.GetChangesByUserAsync(ct));

    [Authorize(Policy = "AuditRead")]
    [HttpGet("changes/by-day")]
    public async Task<IActionResult> ChangesByDay(CancellationToken ct)
        => Ok(await service.GetChangesByDayAsync(ct));

    [Authorize(Policy = "AuditRead")]
    [HttpGet("events/by-type")]
    public async Task<IActionResult> EventsByType(CancellationToken ct)
        => Ok(await service.GetEventsByTypeAsync(ct));

    [Authorize(Policy = "AuditRead")]
    [HttpGet("events/by-system")]
    public async Task<IActionResult> EventsBySystem(CancellationToken ct)
        => Ok(await service.GetEventsBySystemAsync(ct));

    [Authorize(Policy = "AuditRead")]
    [HttpGet("events/by-entity")]
    public async Task<IActionResult> EventsByEntity(CancellationToken ct)
        => Ok(await service.GetEventsByEntityAsync(ct));

    [Authorize(Policy = "AuditRead")]
    [HttpGet("events/by-day")]
    public async Task<IActionResult> EventsByDay(CancellationToken ct)
        => Ok(await service.GetEventsByDayAsync(ct));
}
