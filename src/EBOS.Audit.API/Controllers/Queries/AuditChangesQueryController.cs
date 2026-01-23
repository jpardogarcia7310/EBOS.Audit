using Asp.Versioning;
using EBOS.Audit.Contracts.Filters;
using EBOS.Audit.Infrastructure.Services.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBOS.Audit.Api.Controllers.Queries;

[ApiController]
[ApiVersion(2.0)]
[Route("api/v{version:apiVersion}/audit/changes")]
public sealed class AuditChangesQueryController(AuditQueryService service) : ControllerBase
{
    [Authorize(Policy = "AuditRead")]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] AuditChangeFilter filter, CancellationToken ct)
        => Ok(await service.GetAuditChangesAsync(filter, ct));
}