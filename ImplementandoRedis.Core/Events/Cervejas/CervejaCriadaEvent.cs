using ImplementandoRedis.Core.Events;

namespace ImplementandoRedis.Application.Events.Cervejas;

public record CervejaCriadaEvent(Guid Id, Guid cervejaId) : DomainEvent(Id);
