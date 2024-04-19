using ImplementandoRedis.Core.Entities;

namespace ImplementandoRedis.Core.Events.TiposCerveja;

public sealed record TipoCervejaAtualizadoEvent(Guid Id, TipoCerveja tipoCerveja) : DomainEvent(Id);
