using ImplementandoRedis.Core.Entities;

namespace ImplementandoRedis.Shared.Responses.Cervejas;

public class CriarCervejaResponse
{
    public Guid Id { get; private set; }
    public string Nome { get; set; } = string.Empty;
    public string Fabricante { get; set; } = string.Empty;
    public bool Artesanal { get; set; } = false;
    public TipoCerveja Tipo { get; set; } = null!;
    public string Descricao { get; set; } = string.Empty;
    public int AnoLancamento { get; set; }


    public static implicit operator CriarCervejaResponse(Cerveja cerveja) =>
        new CriarCervejaResponse()
        {
            Id = cerveja.Id,
            Nome = cerveja.Nome,
            Fabricante = cerveja.Fabricante,
            Artesanal = cerveja.Artesanal,
            Tipo = cerveja.Tipo,
            Descricao = cerveja.Descricao,
            AnoLancamento = cerveja.AnoLancamento
        };
}
