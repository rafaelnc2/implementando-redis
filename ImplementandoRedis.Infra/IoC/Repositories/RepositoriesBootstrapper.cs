using ImplementandoRedis.Core.Repositories;
using ImplementandoRedis.Infra.Repositories.EFCore;
using ImplementandoRedis.Infra.Repositories.Redis;
using ImplementandoRedis.Shared.Constants;
using Microsoft.Extensions.DependencyInjection;

namespace ImplementandoRedis.Infra.IoC.Repositories;

public class RepositoriesBootstrapper
{
    public void RepositoriesServiceRegister(IServiceCollection services)
    {
        //services.AddScoped<ICervejaRepository, CervejaEfRepository>();
        services.AddKeyedTransient<ICervejaRepository, CervejaEfRepository>(KeyedServicesName.CERVEJA_EF_REPO);
        services.AddKeyedTransient<ICervejaRepository, CervejaRedisRepository>(KeyedServicesName.CERVEJA_REDIS_REPO);

        //services.AddTransient<ITipoCervejaRepository, TipoCervejaRepository>();
        services.AddKeyedTransient<ITipoCervejaRepository, TipoCervejaEfRepository>(KeyedServicesName.TIPO_CERVEJA_EF_REPO);
        services.AddKeyedTransient<ITipoCervejaRepository, TipoCervejaRedisRepository>(KeyedServicesName.TIPO_CERVEJA_REDIS_REPO);
    }
}
