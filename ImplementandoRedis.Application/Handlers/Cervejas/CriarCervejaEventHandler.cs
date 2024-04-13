using ImplementandoRedis.Application.Events.Cervejas;
using ImplementandoRedis.Core.Constants;
using ImplementandoRedis.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ImplementandoRedis.Application.Handlers.Cervejas;

public sealed class CriarCervejaEventHandler : INotificationHandler<CriarCervejaEvent>
{
    private readonly ILogger<CriarCervejaEventHandler> _logger;
    private readonly ISendMessageService _sender;

    public CriarCervejaEventHandler(ILogger<CriarCervejaEventHandler> logger, ISendMessageService sender)
    {
        _logger = logger;
        _sender = sender;
    }

    public async Task Handle(CriarCervejaEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Enviando cerveja ID {@Id} para a fila de gravação", notification.cerveja.Id);

        var cervejaJson = JsonSerializer.Serialize(notification.cerveja);

        await _sender.SendMessage(ChannelNames.CERVEJA_CRIAR_CHANNEL, cervejaJson);

        _logger.LogInformation("Cerveja ID {@Id} enviada para a fila de gravação", notification.cerveja.Id);
    }
}
