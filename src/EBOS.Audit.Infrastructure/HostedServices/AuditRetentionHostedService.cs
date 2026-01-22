using EBOS.Audit.Infrastructure.Options;
using EBOS.Audit.Application.Services.Retentions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace EBOS.Audit.Infrastructure.HostedServices;

public sealed class AuditRetentionHostedService(IServiceProvider provider, IOptions<AuditRetentionOptions> options)
    : BackgroundService
{
    private readonly AuditRetentionOptions _options = options.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.UtcNow;

            if (now.Hour == _options.RunAtHour)
            {
                using var scope = provider.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<AuditRetentionService>();

                await service.RunRetentionAsync(stoppingToken);
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}