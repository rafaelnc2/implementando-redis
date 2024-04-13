using ImplementandoRedis.Application.Events.TiposCerveja;
using ImplementandoRedis.Core.Constants;
using ImplementandoRedis.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ImplementandoRedis.Application.Handlers.TiposCerveja;

public sealed class AtualizarTipoCervejaEventHandler : INotificationHandler<AtualizarTipoCervejaEvent>
{
    private readonly ILogger<AtualizarTipoCervejaEventHandler> _logger;
    private readonly ISendMessageService _sender;

    public AtualizarTipoCervejaEventHandler(ILogger<AtualizarTipoCervejaEventHandler> logger, ISendMessageService sender)
    {
        _logger = logger;
        _sender = sender;
    }

    public async Task Handle(AtualizarTipoCervejaEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Enviando tipo cerveja ID {@Id} para a fila de atualização", notification.tipoCerveja.Id);

        var tipoCervejaJson = JsonSerializer.Serialize(notification.tipoCerveja);

        await _sender.SendMessage(ChannelNames.TIPO_CERVEJA_ATUALIZAR_CHANNEL, tipoCervejaJson);

        _logger.LogInformation("Tipo cerveja ID {@Id} enviada para a fila de gravação", notification.tipoCerveja.Id);
    }
}
