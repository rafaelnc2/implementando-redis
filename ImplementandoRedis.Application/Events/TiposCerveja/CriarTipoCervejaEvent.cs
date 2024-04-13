namespace ImplementandoRedis.Application.Events.TiposCerveja;

public sealed record CriarTipoCervejaEvent(TipoCerveja tipoCerveja) : INotification;
