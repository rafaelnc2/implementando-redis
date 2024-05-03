namespace ImplementandoRedis.Core.Events.Cervejas;

public record CervejaAtualizadaEvent(Guid Id, Guid cervejaId) : DomainEvent(Id);