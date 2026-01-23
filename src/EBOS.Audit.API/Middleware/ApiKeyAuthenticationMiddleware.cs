using System.Security.Claims;
using EBOS.Audit.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace EBOS.Audit.API.Middleware;

public sealed class ApiKeyAuthenticationMiddleware(RequestDelegate next, IOptions<ApiKeyOptions> options)
{
    private readonly ApiKeyOptions _options = options.Value;

    public async Task InvokeAsync(HttpContext context)
    {
        const string headerName = "X-Api-Key";

        if (!context.Request.Headers.TryGetValue(headerName, out var providedKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Missing API Key");
            return;
        }

        var match = _options.Keys.FirstOrDefault(k => k.Value == providedKey);

        if (match.Key is null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid API Key");
            return;
        }

        // Crear Claims
        var claims = new List<Claim>
        {
            new Claim("system", match.Key),
            new Claim("scope", "audit.write"),
            new Claim("scope", "audit.read")
        };

        var identity = new ClaimsIdentity(claims, "ApiKey");
        var principal = new ClaimsPrincipal(identity);

        context.User = principal;

        await next(context);
    }
}