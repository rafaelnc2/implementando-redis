using ImplementandoRedis.Core.Entities;

namespace ImplementandoRedis.Shared.Responses.TiposCerveja;

public sealed record TipoCervejaResponse(int Id, string Nome, string Origem, string Coloracao, string TeorAlcoolico, string Fermentacao, string Descricao)
{
    public static implicit operator TipoCervejaResponse(TipoCerveja tipoCerveja) =>
        new TipoCervejaResponse(
            Id: tipoCerveja.Id,
            Nome: tipoCerveja.Nome,
            Origem: tipoCerveja.Origem,
            Coloracao: tipoCerveja.Coloracao,
            TeorAlcoolico: tipoCerveja.TeorAlcoolico,
            Fermentacao: tipoCerveja.Fermentacao,
            Descricao: tipoCerveja.Descricao
        );
}