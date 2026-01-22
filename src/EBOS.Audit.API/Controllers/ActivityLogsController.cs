using EBOS.Audit.Application.Contracts.Requests;
using EBOS.Audit.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EBOS.Audit.Api.Controllers;

[ApiController]
[Route("api/audit/activity")]
public sealed class ActivityLogsController(AuditAppService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(
        [FromBody] ActivityLogRequest request,
        CancellationToken ct)
    {
        await service.RegisterActivityAsync(request, ct);
        return Accepted();
    }
}