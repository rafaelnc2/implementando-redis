using System.Text.Json.Serialization;

namespace ImplementandoRedis.Core.Entities;

public abstract class Entity
{
    protected List<string> _errors = new();


    public DateTime? DataAtualizacao { get; set; }
    public DateTime DataCriacao { get; set; }


    [JsonIgnore]
    public IReadOnlyCollection<string> Errors { get => _errors; }
    [JsonIgnore]
    public bool IsValid { get => (_errors.Any() is false); }
}
