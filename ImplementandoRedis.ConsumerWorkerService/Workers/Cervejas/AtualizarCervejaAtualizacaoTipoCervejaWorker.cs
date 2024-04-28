using System.Linq.Expressions;

namespace ImplementandoRedis.ConsumerWorkerService.Workers.Cervejas;

public class AtualizarCervejaAtualizacaoTipoCervejaWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISubscriber _subscriber;
    private readonly ILogger<AtualizarCervejaAtualizacaoTipoCervejaWorker> _logger;

    public AtualizarCervejaAtualizacaoTipoCervejaWorker(
        IServiceProvider serviceProvider, ISubscriber subscriber, ILogger<AtualizarCervejaAtualizacaoTipoCervejaWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _subscriber = subscriber;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Iniciando Worker Atualizar Cerveja com atualização do Tipo de cerveja");

        RedisChannel channel = new(ChannelNames.TIPO_CERVEJA_ATUALIZAR_CHANNEL, RedisChannel.PatternMode.Literal);

        await _subscriber.SubscribeAsync(channel, async (channel, message) =>
        {
            var messageDeserialized = JsonSerializer.Deserialize<TipoCerveja>(message.ToString());

            try
            {
                using var scope = _serviceProvider.CreateScope();

                var cervejaRepo = scope.ServiceProvider.GetRequiredKeyedService<ICervejaRepository>(KeyedServicesName.CERVEJA_EF_REPO);

                Expression<Func<Cerveja, bool>> filter = cerva => cerva.TipoCervejaId == messageDeserialized.Id;

                var cervejas = await cervejaRepo.ObterPorFiltrosAsync(filter);

                //if (messageDeserialized is not TipoCerveja)
                //    return;

                //await tipoCervejaRepo.CriarAsync(messageDeserialized);

                _logger.LogInformation("Mensagem recebida: {Channel} {Id}", channel, messageDeserialized.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na gravação da mensagem: {Channel} {Message}", channel, ex.Message);
            }
        });
    }
}
