namespace ImplementandoRedis.ConsumerWorkerService.Workers.TiposCerveja;

public class CriarTipoCervejaWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISubscriber _subscriber;
    private readonly ILogger<CriarTipoCervejaWorker> _logger;

    public CriarTipoCervejaWorker(IConnectionMultiplexer redisMultiplexerConnect, ILogger<CriarTipoCervejaWorker> logger, IServiceProvider serviceProvider)
    {
        _subscriber = redisMultiplexerConnect.GetSubscriber();
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Iniciando Worker Criar Tipo Cerveja");

        RedisChannel channel = new(ChannelNames.TIPO_CERVEJA_CRIAR_CHANNEL, RedisChannel.PatternMode.Literal);

        await _subscriber.SubscribeAsync(channel, async (channel, message) =>
        {
            var messageDeserialized = JsonSerializer.Deserialize<TipoCerveja>(message.ToString());

            try
            {
                using var scope = _serviceProvider.CreateScope();

                var tipoCervejaRepo = scope.ServiceProvider.GetRequiredKeyedService<ITipoCervejaRepository>(KeyedServicesName.TIPO_CERVEJA_REDIS_REPO);

                if (messageDeserialized is not TipoCerveja)
                    return;

                await tipoCervejaRepo.CriarAsync(messageDeserialized);

                _logger.LogInformation("Mensagem recebida: {Channel} {Id}", channel, messageDeserialized.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na gravação da mensagem: {Channel} {Message}", channel, ex.Message);
            }
        });
    }
}
