using Asp.Versioning;
using EBOS.Audit.Application.Services;
using EBOS.Audit.Infrastructure.DI;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// API Versioning (.NET 8 compatible)
builder.Services.AddApiVersioning(options =>
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Application Services
builder.Services.AddScoped<AuditAppService>();

// Infrastructure
builder.Services.AddAuditInfrastructure(builder.Configuration);

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