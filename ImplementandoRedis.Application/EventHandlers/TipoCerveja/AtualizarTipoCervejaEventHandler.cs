using ImplementandoRedis.Core.Constants;
using ImplementandoRedis.Core.Events.TiposCerveja;
using ImplementandoRedis.Core.Interfaces;

namespace ImplementandoRedis.Application.EventHandlers.TipoCerveja;

public sealed class AtualizarTipoCervejaEventHandler : INotificationHandler<TipoCervejaAtualizadoEvent>
{
    private readonly ILogger<AtualizarTipoCervejaEventHandler> _logger;
    private readonly ISendMessageService _sender;

    public AtualizarTipoCervejaEventHandler(ILogger<AtualizarTipoCervejaEventHandler> logger, ISendMessageService sender)
    {
        _logger = logger;
        _sender = sender;
    }

    public async Task Handle(TipoCervejaAtualizadoEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Enviando tipo cerveja ID {@Id} para a fila de atualização", notification.tipoCerveja.Id);

        var tipoCervejaJson = JsonSerializer.Serialize(notification.tipoCerveja);

        await _sender.SendMessage(ChannelNames.TIPO_CERVEJA_ATUALIZAR_CHANNEL, tipoCervejaJson);

        _logger.LogInformation("Tipo cerveja ID {@Id} enviada para a fila de gravação", notification.tipoCerveja.Id);
    }
}
