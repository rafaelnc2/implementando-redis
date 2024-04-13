using ImplementandoRedis.Infra.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ImplementandoRedis.QueueConsumer.Extensions;

public static class BootstrapperServiceExtensions
{
    public static void AddBootstrapperRegistration(this IServiceCollection services, IConfiguration config)
    {
        new RootBootstrapper().BootstrapperRegisterServices(services, config);
    }
}
