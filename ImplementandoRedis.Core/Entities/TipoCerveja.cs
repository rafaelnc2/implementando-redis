using ImplementandoRedis.Core.Events.TiposCerveja;

namespace ImplementandoRedis.Core.Entities;

[Document(StorageType = StorageType.Json, Prefixes = ["TipoCerveja"])]
public sealed class TipoCerveja : Entity
{
    public TipoCerveja()
    {

    }

    [JsonConstructor]
    private TipoCerveja(string nome, string origem, string coloracao, string teorAlcoolico, string fermentacao, string descricao)
    {
        Id = Id;
        Nome = nome.Trim();
        Origem = origem.Trim();
        Coloracao = coloracao.Trim();
        TeorAlcoolico = teorAlcoolico.Trim();
        Fermentacao = fermentacao.Trim();
        Descricao = descricao;

        DataCriacao = DateTime.Now;
    }

    [JsonInclude]
    [RedisIdField]
    [Indexed]
    public int Id { get; private set; }

    [Indexed]
    public string Nome { get; private set; } = string.Empty;

    [Indexed]
    public string Origem { get; private set; } = string.Empty;

    [Indexed]
    public string Coloracao { get; private set; } = string.Empty;

    [Indexed]
    public string TeorAlcoolico { get; private set; } = string.Empty;

    [Indexed]
    public string Fermentacao { get; private set; } = string.Empty;

    [Searchable]
    public string Descricao { get; private set; } = string.Empty;

    [JsonIgnore]
    public IEnumerable<Cerveja>? Cerveja { get; set; }



    private static void Validate(string nome, string origem, string coloracao, string teorAlcoolico, string fermentacao, string descricao)
    {
        if (string.IsNullOrWhiteSpace(nome))
            _errors.Add("Nome é obrigatório");

        if (string.IsNullOrEmpty(nome) is false && nome.Length > 50)
            _errors.Add("Nome deve ter no máximo 50 caracteres");

        if (string.IsNullOrEmpty(origem) is false && origem.Length > 50)
            _errors.Add("Origem deve ter no máximo 50 caracteres");

        if (string.IsNullOrEmpty(coloracao) is false && coloracao.Length > 50)
            _errors.Add("Coloracao deve ter no máximo 50 caracteres");

        if (string.IsNullOrEmpty(teorAlcoolico) is false && teorAlcoolico.Length > 50)
            _errors.Add("Teor Alcoólico deve ter no máximo 50 caracteres");

        if (string.IsNullOrEmpty(fermentacao) is false && fermentacao.Length > 50)
            _errors.Add("Fermentação deve ter no máximo 50 caracteres");

        if (string.IsNullOrWhiteSpace(descricao))
            _errors.Add("Descrição é obrigatório");

        if (string.IsNullOrWhiteSpace(descricao) is false && descricao.Length > 1000)
            _errors.Add("Descrição deve ter no máximo 1000 caracteres");
    }


    public static TipoCerveja? Create(string nome, string origem, string coloracao, string teorAlcoolico, string fermentacao, string descricao)
    {
        Validate(nome, origem, coloracao, teorAlcoolico, fermentacao, descricao);

        if (_errors.Any() is true)
            return null;

        var tipoCerveja = new TipoCerveja(
            nome,
            origem,
            coloracao,
            teorAlcoolico,
            fermentacao,
            descricao
        );

        Raise(new TipoCervejaAtualizadoEvent(Guid.NewGuid(), tipoCerveja));

        return tipoCerveja;
    }

    public void Update(string nome, string origem, string coloracao, string teorAlcoolico, string fermentacao, string descricao)
    {
        Validate(nome, origem, coloracao, teorAlcoolico, fermentacao, descricao);

        if (_errors.Any() is true)
            return;

        Nome = nome;
        Origem = origem;
        Coloracao = coloracao;
        TeorAlcoolico = teorAlcoolico;
        Fermentacao = fermentacao;
        Descricao = descricao;

        DataAtualizacao = DateTime.Now;

        Raise(new TipoCervejaAtualizadoEvent(Guid.NewGuid(), this));
    }
}