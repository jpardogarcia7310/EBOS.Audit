using Asp.Versioning;
using EBOS.Audit.Application.Services.Queries;
using EBOS.Audit.Contracts.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EBOS.Audit.Api.Controllers.Queries;

[ApiController]
[ApiVersion(2.0)]
[Route("api/v{version:apiVersion}/audit/events")]
public sealed class DomainEventsQueryController(AuditQueryService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] DomainEventLogFilter filter, CancellationToken ct)
        => Ok(await service.GetDomainEventsAsync(filter, ct));
}