using EBOS.Audit.Application.Contracts.Requests;
using EBOS.Audit.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EBOS.Audit.Api.Controllers;

[ApiController]
[Route("api/audit/changes")]
public sealed class AuditChangesController(AuditAppService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AuditChangeRequest request, CancellationToken ct)
    {
        await service.RegisterChangeAsync(request, ct);
        return Accepted();
    }
}