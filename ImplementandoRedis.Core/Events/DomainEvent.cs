using MediatR;

namespace ImplementandoRedis.Core.Events;

public record DomainEvent(Guid Id) : INotification;
