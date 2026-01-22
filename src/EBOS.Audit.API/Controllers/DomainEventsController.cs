using Asp.Versioning;
using EBOS.Audit.Application.Contracts.Requests;
using EBOS.Audit.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EBOS.Audit.Api.Controllers;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/audit/events")]
public sealed class DomainEventsController(AuditAppService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DomainEventRequest request, CancellationToken ct)
    {
        await service.RegisterEventAsync(request, ct);
        return Accepted();
    }
}