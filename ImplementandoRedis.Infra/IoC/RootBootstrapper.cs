using CacheSample.Infra.DataAccess.EFCore.Context;
using ImplementandoRedis.Infra.Interceptors;
using ImplementandoRedis.Infra.IoC.Contexts;
using ImplementandoRedis.Infra.IoC.Repositories;
using ImplementandoRedis.Infra.IoC.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ImplementandoRedis.Infra.IoC;

public class RootBootstrapper
{
    public void BootstrapperRegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IConnectionMultiplexer>(opt => ConnectionMultiplexer.Connect(config.GetConnectionString("Redis") ?? string.Empty));

        services.AddDbContext<DataContext>((serviceProvider, opt) => opt
            .UseSqlServer(config.GetConnectionString("SqlServer"))
            //options =>
            //{
            //    options.EnableRetryOnFailure(
            //        maxRetryCount: 10,
            //        maxRetryDelay: TimeSpan.FromSeconds(5),
            //        errorNumbersToAdd: null
            //    );
            //}
            //)
            .AddInterceptors(serviceProvider.GetRequiredService<PublishDomainEventsInterceptor>())
        );

        new RepositoriesBootstrapper().RepositoriesServiceRegister(services);

        new ServicesBootstrapper().ServicesServiceRegister(services);

        new ContextsBootstrapper().ContextsServiceRegister(services);
    }

}
