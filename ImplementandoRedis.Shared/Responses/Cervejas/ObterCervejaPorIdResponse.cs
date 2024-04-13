using ImplementandoRedis.Core.Entities;
using ImplementandoRedis.Shared.Responses.TiposCerveja;

namespace ImplementandoRedis.Shared.Responses.Cervejas;

public sealed record ObterCervejaPorIdResponse(string Nome, string Fabricante, bool Artesanal, string Descricao, int AnoLancamento, int TipoId, TipoCervejaResponse Tipo)
{
    public static implicit operator ObterCervejaPorIdResponse(Cerveja cerveja) =>
        new ObterCervejaPorIdResponse(
            Nome: cerveja.Nome,
            Fabricante: cerveja.Fabricante,
            Artesanal: cerveja.Artesanal,
            Descricao: cerveja.Descricao,
            AnoLancamento: cerveja.AnoLancamento,
            TipoId: cerveja.TipoId,
            Tipo: cerveja.Tipo
        );
}
