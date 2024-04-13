using ImplementandoRedis.Infra.IoC;

namespace ImplementandoRedis.Api.Extensions;

public static class BootstrapperServiceExtensions
{
    public static void AddBootstrapperRegistration(this IServiceCollection services, IConfiguration config)
    {
        new RootBootstrapper().BootstrapperRegisterServices(services, config);
    }
}
