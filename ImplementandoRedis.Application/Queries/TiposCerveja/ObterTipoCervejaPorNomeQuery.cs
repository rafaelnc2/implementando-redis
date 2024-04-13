using ImplementandoRedis.Shared.Responses.TiposCerveja;

namespace ImplementandoRedis.Application.Queries.TiposCerveja;

public sealed record ObterTipoCervejaPorFiltrosQuery(
    string Nome, string Origem, string Coloracao, string TeorAlcoolico, string Fermentacao
) : IRequest<CustomResult<IEnumerable<TipoCervejaResponse>>>;
