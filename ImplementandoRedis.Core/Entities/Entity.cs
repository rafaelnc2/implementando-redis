using ImplementandoRedis.Core.Events;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ImplementandoRedis.Core.Entities;

public abstract class Entity
{
    protected List<DomainEvent> _domainEvents = new();
    protected List<string> _errors = new();


    public DateTime? DataAtualizacao { get; set; }
    public DateTime DataCriacao { get; set; }


    [JsonIgnore]
    [NotMapped]
    public IReadOnlyCollection<DomainEvent> DomainEvents { get => _domainEvents; }

    [JsonIgnore]
    [NotMapped]
    public IReadOnlyCollection<string> Errors { get => _errors; }

    [JsonIgnore]
    [NotMapped]
    public bool IsValid { get => (_errors.Any() is false); }


    protected void Raise(DomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);
}
