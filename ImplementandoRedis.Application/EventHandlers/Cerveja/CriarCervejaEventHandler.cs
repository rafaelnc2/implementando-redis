using ImplementandoRedis.Application.Events.Cervejas;
using ImplementandoRedis.Core.Constants;
using ImplementandoRedis.Core.Interfaces;

namespace ImplementandoRedis.Application.EventHandlers.Cerveja;

public sealed class CriarCervejaEventHandler : INotificationHandler<CervejaCriadaEvent>
{
    private readonly ILogger<CriarCervejaEventHandler> _logger;
    private readonly ISendMessageService _sender;

    public CriarCervejaEventHandler(ILogger<CriarCervejaEventHandler> logger, ISendMessageService sender)
    {
        _logger = logger;
        _sender = sender;
    }

    public async Task Handle(CervejaCriadaEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Enviando cerveja ID {@Id} para a fila de gravação", notification.cervejaId);

        await _sender.SendMessage(ChannelNames.CERVEJA_CRIAR_CHANNEL, notification.cervejaId.ToString());

        _logger.LogInformation("Cerveja ID {@Id} enviada para a fila de gravação", notification.cervejaId);
    }
}
