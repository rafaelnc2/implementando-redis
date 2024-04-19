using ImplementandoRedis.Core.Entities;

namespace ImplementandoRedis.Core.Events.TiposCerveja;

public sealed record TipoCervejaCriadoEvent(Guid Id, TipoCerveja tipoCerveja) : DomainEvent(Id);
