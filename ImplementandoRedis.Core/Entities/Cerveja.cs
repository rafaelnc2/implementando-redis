using ImplementandoRedis.Application.Events.Cervejas;

namespace ImplementandoRedis.Core.Entities;

[Document(StorageType = StorageType.Json, Prefixes = ["Cerveja"])]
public sealed class Cerveja : Entity
{
    public Cerveja()
    {

    }

    [JsonConstructor]
    private Cerveja(string nome, string fabricante, bool artesanal, string descricao, int anoLancamento, int tipoCervejaId, TipoCerveja tipoCerveja)
    {
        Id = Guid.NewGuid();

        Nome = nome;
        Fabricante = fabricante;
        Artesanal = artesanal;
        Descricao = descricao;
        AnoLancamento = anoLancamento;

        TipoCervejaId = tipoCervejaId;
        TipoCerveja = tipoCerveja;

        DataCriacao = DateTime.Now;
    }

    public static Cerveja? Create(string nome, string fabricante, bool artesanal, string descricao, int anoLancamento, int tipoCervejaId, TipoCerveja tipoCerveja)
    {
        Validate(nome, fabricante);

        if (_errors.Any() is false)
            return null;

        var cerveja = new Cerveja(
            nome,
            fabricante,
            artesanal,
            descricao,
            anoLancamento,
            tipoCervejaId,
            tipoCerveja
        );

        Raise(new CervejaCriadaEvent(Guid.NewGuid(), cerveja));

        return cerveja;
    }


    [RedisIdField]
    [Indexed]
    public Guid Id { get; set; }

    [Indexed]
    public string Nome { get; private set; } = string.Empty;

    [Indexed]
    public string Fabricante { get; private set; } = string.Empty;

    [Indexed]
    public bool Artesanal { get; private set; }

    public int TipoCervejaId { get; private set; }

    [Searchable]
    public string Descricao { get; private set; } = string.Empty;

    [Indexed]
    public int AnoLancamento { get; private set; }


    [Indexed]
    public TipoCerveja? TipoCerveja { get; private set; }


    private static void Validate(string nome, string fabricante)
    {
        if (string.IsNullOrWhiteSpace(nome))
            _errors.Add("Nome é obrigatório");

        if (nome is not null && nome.Length > 50)
            _errors.Add("Nome deve ter no máximo 50 caracteres");

        if (string.IsNullOrWhiteSpace(fabricante))
            _errors.Add("Fabricante é obrigatório");

        if (fabricante is not null && fabricante.Length > 100)
            _errors.Add("Fabricante deve ter no máximo 100 caracteres");
    }
}
