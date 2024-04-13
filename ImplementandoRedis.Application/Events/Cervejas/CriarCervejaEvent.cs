namespace ImplementandoRedis.Application.Events.Cervejas;

public record CriarCervejaEvent(Cerveja cerveja) : INotification;
