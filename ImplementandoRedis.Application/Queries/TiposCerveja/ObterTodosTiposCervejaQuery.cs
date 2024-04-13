using ImplementandoRedis.Shared.Responses.TiposCerveja;

namespace ImplementandoRedis.Application.Queries.TiposCerveja;

public sealed record ObterTodosTiposCervejaQuery() : IRequest<CustomResult<IEnumerable<TipoCervejaResponse>>>;
