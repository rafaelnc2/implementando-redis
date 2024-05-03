using ImplementandoRedis.Core.Constants;
using ImplementandoRedis.Core.Events.Cervejas;
using ImplementandoRedis.Core.Interfaces;

namespace ImplementandoRedis.Application.EventHandlers.Cerveja;

public sealed class AtualizarCervejaEventHandler : INotificationHandler<CervejaAtualizadaEvent>
{
    private readonly ILogger<AtualizarCervejaEventHandler> _logger;
    private readonly ISendMessageService _sender;

    public AtualizarCervejaEventHandler(ILogger<AtualizarCervejaEventHandler> logger, ISendMessageService sender)
    {
        _logger = logger;
        _sender = sender;
    }

    public async Task Handle(CervejaAtualizadaEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Enviando cerveja ID {@Id} para a fila de atualização", notification.cervejaId);

        await _sender.SendMessage(ChannelNames.CERVEJA_ATUALIZAR_CHANNEL, notification.cervejaId.ToString());

        _logger.LogInformation("Cerveja ID {@Id} enviada para a fila de atualização", notification.cervejaId);
    }
}
