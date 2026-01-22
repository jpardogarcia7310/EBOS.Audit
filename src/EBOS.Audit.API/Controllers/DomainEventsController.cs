using EBOS.Audit.Application.Contracts.Requests;
using EBOS.Audit.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EBOS.Audit.Api.Controllers;

[ApiController]
[Route("api/audit/events")]
public sealed class DomainEventsController(AuditAppService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(
        [FromBody] DomainEventRequest request,
        CancellationToken ct)
    {
        await service.RegisterEventAsync(request, ct);
        return Accepted();
    }
}