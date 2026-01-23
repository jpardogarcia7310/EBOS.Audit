using Asp.Versioning;
using EBOS.Audit.Client.Contracts;
using EBOS.Audit.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBOS.Audit.Api.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiVersion(2.0)]
[Route("api/v{version:apiVersion}/audit/changes")]
[Produces("application/json")]
public sealed class AuditChangesController(AuditAppService service) : ControllerBase
{
    [Authorize(Policy = "AuditWrite")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AuditChangeRequest request, CancellationToken ct)
    {
        await service.RegisterChangeAsync(request, ct);
        return Accepted();
    }
}

