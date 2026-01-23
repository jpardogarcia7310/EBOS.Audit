using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using EBOS.Audit.API.Middleware;
using EBOS.Audit.Infrastructure;
using EBOS.Audit.Infrastructure.DI;
using EBOS.Audit.Infrastructure.HostedServices;
using EBOS.Audit.Infrastructure.Options;
using EBOS.Audit.Infrastructure.Services;
using EBOS.Audit.Infrastructure.Services.Queries;
using EBOS.Audit.Infrastructure.Services.Retentions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// -----------------------------------------------------------------------------
// Infrastructure
// ----------------------------------------------------------------------------
services.AddInfrastructure(configuration);
services.AddAuditInfrastructure(configuration);

// -----------------------------------------------------------------------------
// Controllers
// -----------------------------------------------------------------------------
services.AddControllers();

// -----------------------------------------------------------------------------
// CORS
// -----------------------------------------------------------------------------
services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// -----------------------------------------------------------------------------
// API Versioning (.NET 8 supported)
// -----------------------------------------------------------------------------
services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV"; // v1, v2, v3
        options.SubstituteApiVersionInUrl = true;
    });
// Required for Swagger with versioning
services.AddEndpointsApiExplorer(); 

// -----------------------------------------------------------------------------
// Swagger (sin BuildServiceProvider, sin warnings)
// -----------------------------------------------------------------------------
services.AddSwaggerGen(); 
services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

// -----------------------------------------------------------------------------
// Application Services
// -----------------------------------------------------------------------------
services.AddScoped<AuditAppService>();
services.AddScoped<AuditQueryService>();
services.AddScoped<AuditAggregationService>();
// Retention settings
services.Configure<AuditRetentionOptions>(configuration.GetSection("AuditRetention"));
services.AddScoped<AuditRetentionService>(); 
services.AddHostedService<AuditRetentionHostedService>();

// -----------------------------------------------------------------------------
// API Keys
// -----------------------------------------------------------------------------
services.Configure<ApiKeyOptions>(configuration.GetSection("ApiKeys"));

// -----------------------------------------------------------------------------
// Authorization Policies
// -----------------------------------------------------------------------------
services.AddAuthorizationBuilder() 
    .AddPolicy("AuditWrite", policy => policy.RequireClaim("scope", "audit.write")) 
    .AddPolicy("AuditRead", policy => policy.RequireClaim("scope", "audit.read"));

// -----------------------------------------------------------------------------
// Build App
// -----------------------------------------------------------------------------
var app = builder.Build();

// -----------------------------------------------------------------------------
// Middleware
// -----------------------------------------------------------------------------
app.UseCors("AllowAll");

// Swagger solo en Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint( $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseMiddleware<ApiKeyAuthenticationMiddleware>();

// -----------------------------------------------------------------------------
// Endpoints
// -----------------------------------------------------------------------------
app.MapControllers(); 

// -----------------------------------------------------------------------------
// Run
// -----------------------------------------------------------------------------
await app.RunAsync();

// ============================================================================
// Swagger Options configurator (evita BuildServiceProvider y warnings ASP0000)
// ============================================================================
public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                new OpenApiInfo
                {
                    Title = "EBOS.Audit API",
                    Version = description.ApiVersion.ToString(),
                    Description = "EBOS audit service with versioning"
                });
        }
    }
}

//services.AddSwaggerGen(options =>
//{
//    var provider = services.BuildServiceProvider()
//        .GetRequiredService<IApiVersionDescriptionProvider>();
//
//    foreach (var description in provider.ApiVersionDescriptions)
//    {
//        options.SwaggerDoc(
//            description.GroupName,
//            new OpenApiInfo
//            {
//                Title = "EBOS.Audit API",
//                Version = description.ApiVersion.ToString(),
//                Description = "EBOS audit service with versioning"
//            });
//    }
//});

