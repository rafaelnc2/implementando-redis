namespace ImplementandoRedis.ConsumerWorkerService.Workers.Cervejas;

public class CriarCervejaWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISubscriber _subscriber;
    private readonly ILogger<CriarCervejaWorker> _logger;

    public CriarCervejaWorker(IConnectionMultiplexer redisMultiplexerConnect, ILogger<CriarCervejaWorker> logger, IServiceProvider serviceProvider)
    {
        _subscriber = redisMultiplexerConnect.GetSubscriber();
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Iniciando Worker Criar Cerveja");

        RedisChannel channel = new(ChannelNames.CERVEJA_CRIAR_CHANNEL, RedisChannel.PatternMode.Literal);

        await _subscriber.SubscribeAsync(channel, async (channel, message) =>
        {
            var messageDeserialized = JsonSerializer.Deserialize<Cerveja>(message.ToString());

            try
            {
                using var scope = _serviceProvider.CreateScope();

                var cervejaRepo = scope.ServiceProvider.GetRequiredKeyedService<ICervejaRepository>(KeyedServicesName.CERVEJA_EF_REPO);

                await cervejaRepo.CriarAsync(messageDeserialized);

                _logger.LogInformation("Mensagem recebida: {Channel} {Id}", channel, messageDeserialized?.Id);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Erro na gravação da mensagem: {Channel} {Id}", channel, messageDeserialized?.Id);
            }
        });
    }
}
