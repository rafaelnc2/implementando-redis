
using ImplementandoRedis.Core.Interfaces;

namespace ImplementandoRedis.ConsumerWorkerService.Workers.TiposCerveja;

public class AtualizarTipoCervejaWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISubscriber _subscriber;
    private readonly ILogger<CriarTipoCervejaWorker> _logger;
    private readonly ISendMessageService _sender;


    public AtualizarTipoCervejaWorker(IConnectionMultiplexer redisMultiplexerConnect, ILogger<CriarTipoCervejaWorker> logger,
        IServiceProvider serviceProvider, ISendMessageService sender)
    {
        _subscriber = redisMultiplexerConnect.GetSubscriber();
        _logger = logger;
        _serviceProvider = serviceProvider;
        _sender = sender;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Iniciando Worker Atualizar Tipo Cerveja");

        RedisChannel channel = new(ChannelNames.TIPO_CERVEJA_ATUALIZAR_CHANNEL, RedisChannel.PatternMode.Literal);

        await _subscriber.SubscribeAsync(channel, async (channel, message) =>
        {
            var messageDeserialized = JsonSerializer.Deserialize<TipoCerveja>(message.ToString());

            try
            {
                using var scope = _serviceProvider.CreateScope();

                var tipoCervejaRepo = scope.ServiceProvider.GetRequiredKeyedService<ITipoCervejaRepository>(KeyedServicesName.TIPO_CERVEJA_REDIS_REPO);

                if (messageDeserialized is not TipoCerveja)
                    return;

                var tipoCerveja = await tipoCervejaRepo.ObterPorIdAsync(messageDeserialized.Id);

                if (tipoCerveja is null)
                {
                    _logger.LogWarning("Tipo cerveja não encontrada. Encaminhando para a fila de inclusão no Redis: {Channel} {Id}", channel, messageDeserialized.Id);

                    await _sender.SendMessage(ChannelNames.TIPO_CERVEJA_CRIAR_CHANNEL, message.ToString());

                    return;
                }

                await tipoCervejaRepo.AtualizarAsync(messageDeserialized);

                _logger.LogInformation("Mensagem recebida: {Channel} {Id}", channel, messageDeserialized.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na gravação da mensagem: {Channel} {Message}", channel, ex.Message);
            }
        });
    }
}
