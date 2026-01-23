using Asp.Versioning;
using EBOS.Audit.Client.Contracts;
using EBOS.Audit.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBOS.Audit.Api.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiVersion(2.0)]
[Route("api/v{version:apiVersion}/audit/events")]
[Produces("application/json")]
public sealed class DomainEventsController(AuditAppService service) : ControllerBase
{
    [Authorize(Policy = "AuditWrite")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DomainEventRequest request, CancellationToken ct)
    {
        await service.RegisterEventAsync(request, ct);
        return Accepted();
    }
}