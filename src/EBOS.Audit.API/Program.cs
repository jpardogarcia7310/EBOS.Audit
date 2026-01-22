using Asp.Versioning;
using EBOS.Audit.Application.Services;
using EBOS.Audit.Application.Services.Queries;
using EBOS.Audit.Application.Services.Retentions;
using EBOS.Audit.Infrastructure;
using EBOS.Audit.Infrastructure.DI;
using EBOS.Audit.Infrastructure.HostedServices;
using EBOS.Audit.Infrastructure.Options;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddInfrastructure(builder.Configuration);

// Controllers
services.AddControllers();

// CORS
services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// API Versioning (.NET 8 compatible)
services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

// Swagger
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// Application Services
services.AddScoped<AuditAppService>();

// Infrastructure
services.AddAuditInfrastructure(builder.Configuration);

services.AddScoped<AuditQueryService>();
services.AddScoped<AuditAggregationService>();
// Retention settings
services.Configure<AuditRetentionOptions>( 
    builder.Configuration.GetSection("AuditRetention"));
services.AddScoped<AuditRetentionService>(); 
services.AddHostedService<AuditRetentionHostedService>();

var app = builder.Build();

// CORS
app.UseCors("AllowAll");

// Swagger solo en Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "EBOS.Audit API v1");
        options.DocumentTitle = "EBOS.Audit - API Documentation";
    });
}

app.MapControllers();

app.Run();