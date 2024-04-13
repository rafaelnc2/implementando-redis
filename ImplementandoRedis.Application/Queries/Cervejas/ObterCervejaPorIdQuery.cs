using ImplementandoRedis.Shared.Responses.Cervejas;

namespace ImplementandoRedis.Application.Queries.Cervejas;

public sealed record ObterCervejaPorIdQuery(Guid Id) : IRequest<CustomResult<ObterCervejaPorIdResponse>>;
