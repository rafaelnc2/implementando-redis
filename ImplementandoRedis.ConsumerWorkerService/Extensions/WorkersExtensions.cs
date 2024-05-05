using ImplementandoRedis.ConsumerWorkerService.Workers.Cervejas;
using ImplementandoRedis.ConsumerWorkerService.Workers.TiposCerveja;

namespace ImplementandoRedis.ConsumerWorkerService.Extensions;

public static class WorkersExtensions
{
    public static void AddWorkers(this IServiceCollection services)
    {
        services.AddHostedService<CriarCervejaWorker>();
        services.AddHostedService<AtualizarCervejaWorker>();
        services.AddHostedService<CriarTipoCervejaWorker>();
        services.AddHostedService<AtualizarTipoCervejaWorker>();
    }
}
