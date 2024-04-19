using ImplementandoRedis.Core.Entities;
using ImplementandoRedis.Core.Events;

namespace ImplementandoRedis.Application.Events.Cervejas;

public record CervejaCriadaEvent(Guid Id, Cerveja cerveja) : DomainEvent(Id);
