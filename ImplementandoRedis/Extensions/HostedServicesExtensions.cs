using ImplementandoRedis.Api.HostedServices;

namespace ImplementandoRedis.Api.Extensions;

public static class HostedServicesExtensions
{
    public static void AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<RedisIndexCreationService>();
    }
}
