using Asp.Versioning;
using EBOS.Audit.Contracts.Filters;
using EBOS.Audit.Infrastructure.Services.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBOS.Audit.Api.Controllers.Queries;

[ApiController]
[ApiVersion(2.0)]
[Route("api/v{version:apiVersion}/audit/activity")]
public sealed class ActivityLogsQueryController(AuditQueryService service) : ControllerBase
{
    [Authorize(Policy = "AuditRead")]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] ActivityLogFilter filter, CancellationToken ct)
        => Ok(await service.GetActivityLogsAsync(filter, ct));
}