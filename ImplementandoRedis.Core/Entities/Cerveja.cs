using ImplementandoRedis.Application.Events.Cervejas;
using ImplementandoRedis.Core.Events.Cervejas;

namespace ImplementandoRedis.Core.Entities;

[Document(StorageType = StorageType.Json, Prefixes = ["Cerveja"])]
public sealed class Cerveja : Entity
{
    public Cerveja()
    {

    }

    [JsonConstructor]
    private Cerveja(string nome, string fabricante, bool artesanal, string descricao, string harmonizacao, int anoLancamento, TipoCerveja tipoCerveja, DateTime dataCriacao, DateTime? dataAtualizacao)
    {
        Id = Guid.NewGuid();

        Nome = nome;
        Fabricante = fabricante;
        Artesanal = artesanal;
        Descricao = descricao;
        Harmonizacao = harmonizacao;
        AnoLancamento = anoLancamento;

        TipoCervejaId = tipoCerveja.Id;
        TipoCerveja = tipoCerveja;

        DataCriacao = dataCriacao;
        DataAtualizacao = dataAtualizacao.HasValue ? dataAtualizacao.Value : null;
    }

    [RedisIdField]
    [Indexed]
    public Guid Id { get; set; }

    [Searchable]
    public string Nome { get; private set; } = string.Empty;

    [Indexed]
    public string Fabricante { get; private set; } = string.Empty;

    [Indexed]
    public bool Artesanal { get; private set; }

    public int TipoCervejaId { get; private set; }

    [Searchable]
    public string Descricao { get; private set; } = string.Empty;

    [Searchable]
    public string Harmonizacao { get; private set; } = string.Empty;

    [Indexed]
    public int AnoLancamento { get; private set; }


    [Indexed]
    public TipoCerveja TipoCerveja { get; private set; }



    public static Cerveja? Create(string nome, string fabricante, bool artesanal, string descricao, string harmonizacao, int anoLancamento, TipoCerveja tipoCerveja)
    {
        Validate(nome, fabricante, descricao, harmonizacao);

        if (_errors.Any() is true)
            return null;

        var cerveja = new Cerveja(
            nome,
            fabricante,
            artesanal,
            descricao,
            harmonizacao,
            anoLancamento,
            tipoCerveja,
            dataCriacao: DateTime.Now,
            dataAtualizacao: null
        );

        Raise(new CervejaCriadaEvent(Guid.NewGuid(), cerveja.Id));

        return cerveja;
    }

    public void Update(string nome, string fabricante, bool artesanal, string descricao, string harmonizacao, int anoLancamento, TipoCerveja tipoCerveja)
    {
        Validate(nome, fabricante, descricao, harmonizacao);

        if (_errors.Any() is true)
            return;

        Nome = nome;
        Fabricante = fabricante;
        Artesanal = artesanal;
        Descricao = descricao;
        Harmonizacao = harmonizacao;
        AnoLancamento = anoLancamento;

        TipoCervejaId = tipoCerveja.Id;
        TipoCerveja = tipoCerveja;

        DataAtualizacao = DateTime.Now;

        Raise(new CervejaAtualizadaEvent(Guid.NewGuid(), Id));
    }

    private static void Validate(string nome, string fabricante, string descricao, string harmonizacao)
    {
        if (string.IsNullOrWhiteSpace(nome))
            _errors.Add("Nome é obrigatório");

        if (nome is not null && nome.Length > 50)
            _errors.Add("Nome deve ter no máximo 50 caracteres");

        if (string.IsNullOrWhiteSpace(fabricante))
            _errors.Add("Fabricante é obrigatório");

        if (fabricante is not null && fabricante.Length > 100)
            _errors.Add("Fabricante deve ter no máximo 100 caracteres");

        if (descricao is not null && descricao.Length > 1000)
            _errors.Add("Descrição deve ter no máximo 1000 caracteres");

        if (harmonizacao is not null && harmonizacao.Length > 1000)
            _errors.Add("Armonizacao deve ter no máximo 1000 caracteres");
    }

}