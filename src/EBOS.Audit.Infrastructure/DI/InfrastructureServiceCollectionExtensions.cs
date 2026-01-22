using EBOS.Audit.Domain.Interfaces.Repositories;
using EBOS.Audit.Domain.Interfaces.Services;
using EBOS.Audit.Infrastructure.Persistence;
using EBOS.Audit.Infrastructure.Repositories;
using EBOS.Audit.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EBOS.Audit.Infrastructure.DI;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddAuditInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AuditDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AuditConnection")));

        services.AddScoped<IAuditChangeRepository, AuditChangeRepository>();
        services.AddScoped<IDomainEventLogRepository, DomainEventLogRepository>();
        services.AddScoped<IActivityLogRepository, ActivityLogRepository>();

        services.AddScoped<IAuditService, AuditService>();

        return services;
    }
}