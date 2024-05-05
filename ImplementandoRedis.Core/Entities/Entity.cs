using ImplementandoRedis.Core.Events;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImplementandoRedis.Core.Entities;

public abstract class Entity
{
    protected static List<DomainEvent> _domainEvents = new();
    protected static List<string> _errors = new();


    public DateTime? DataAtualizacao { get; protected set; }
    public DateTime DataCriacao { get; protected set; }


    [JsonIgnore]
    [NotMapped]
    public IReadOnlyCollection<DomainEvent> DomainEvents { get => _domainEvents; }

    [JsonIgnore]
    [NotMapped]
    public IReadOnlyCollection<string> Errors { get => _errors; }

    [JsonIgnore]
    [NotMapped]
    public bool IsValid { get => (_errors.Any() is false); }


    protected static void Raise(DomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);

    public void ClearEvents() => _domainEvents.Clear();
}
