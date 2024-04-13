using ImplementandoRedis.Shared.Responses.TiposCerveja;

namespace ImplementandoRedis.Application.Queries.TiposCerveja;

public sealed record ObterTipoCervejaPorIdQuery(int Id) : IRequest<CustomResult<TipoCervejaResponse>>;
