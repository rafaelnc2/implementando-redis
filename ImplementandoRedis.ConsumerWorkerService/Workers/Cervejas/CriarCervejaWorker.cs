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
            Guid cervejaId;

            if (Guid.TryParse(message.ToString(), out cervejaId) is false)
            {
                _logger.LogError("ID inválido: {Channel} {Id}", channel, message.ToString());
                return;
            }

            try
            {
                using var scope = _serviceProvider.CreateScope();

                var cervejaRedisRepo = scope.ServiceProvider.GetRequiredKeyedService<ICervejaRepository>(KeyedServicesName.CERVEJA_REDIS_REPO);

                var cerveja = await cervejaRedisRepo.ObterPorIdAsync(cervejaId);

                if (cerveja is null)
                {
                    _logger.LogError("ID não encontrado no REDIS: {Channel} {Id}", channel, message.ToString());
                    return;
                }

                var cervejaRepo = scope.ServiceProvider.GetRequiredKeyedService<ICervejaRepository>(KeyedServicesName.CERVEJA_EF_REPO);

                await cervejaRepo.CriarAsync(cerveja);

                _logger.LogInformation("Mensagem recebida: {Channel} {Id}", channel, cervejaId.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na gravação da mensagem: {Channel} {Id}", channel, cervejaId.ToString());
                _logger.LogError(ex.Message);
            }
        });
    }
}
