namespace ImplementandoRedis.Application.Events.TiposCerveja;

public sealed record AtualizarTipoCervejaEvent(TipoCerveja tipoCerveja) : INotification;
