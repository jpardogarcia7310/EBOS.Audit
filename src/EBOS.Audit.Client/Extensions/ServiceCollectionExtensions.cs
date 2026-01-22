using EBOS.Audit.Client.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EBOS.Audit.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuditClient(
        this IServiceCollection services,
        Action<AuditClientOptions> configure)
    {
        services.Configure(configure);

        services.AddHttpClient<IAuditClient, AuditClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<AuditClientOptions>>().Value;

            client.BaseAddress = new Uri(options.BaseUrl);

            if (!string.IsNullOrWhiteSpace(options.ApiKey))
                client.DefaultRequestHeaders.Add("X-Api-Key", options.ApiKey);
        });

        return services;
    }
}