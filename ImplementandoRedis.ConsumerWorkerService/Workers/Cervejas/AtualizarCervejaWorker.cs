namespace ImplementandoRedis.ConsumerWorkerService.Workers.Cervejas;

public class AtualizarCervejaWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISubscriber _subscriber;
    private readonly ILogger<CriarCervejaWorker> _logger;

    public AtualizarCervejaWorker(IConnectionMultiplexer redisMultiplexerConnect, IServiceProvider serviceProvider, ILogger<CriarCervejaWorker> logger)
    {
        _subscriber = redisMultiplexerConnect.GetSubscriber();
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Iniciando Worker Atualizar Cerveja");

        RedisChannel channel = new(ChannelNames.CERVEJA_ATUALIZAR_CHANNEL, RedisChannel.PatternMode.Literal);

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

                var cervejaRedis = await cervejaRedisRepo.ObterPorIdAsync(cervejaId);

                if (cervejaRedis is null)
                {
                    _logger.LogError("ID não encontrado no REDIS: {Channel} {Id}", channel, cervejaId.ToString());
                    return;
                }

                var cervejaEfRepo = scope.ServiceProvider.GetRequiredKeyedService<ICervejaRepository>(KeyedServicesName.CERVEJA_EF_REPO);

                var cervejaEf = await cervejaEfRepo.ObterPorIdNoTrackAsync(cervejaRedis.Id);

                if (cervejaEf is null)
                {
                    _logger.LogError("ID não encontrado no MSSQL: {Channel} {Id}", channel, cervejaId.ToString());
                    return;
                }

                await cervejaEfRepo.AtualizarAsync(cervejaRedis);

                _logger.LogInformation("Cerveja atualizada com sucesso: {Channel} {Id}", channel, cervejaId.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro na atualização da cerveja: {Channel} {Id}", channel, cervejaId.ToString());
                _logger.LogError(ex.Message);
            }
        });
    }
}
