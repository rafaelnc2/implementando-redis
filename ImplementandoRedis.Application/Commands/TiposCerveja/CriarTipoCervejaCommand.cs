using ImplementandoRedis.Shared.Responses.TiposCerveja;

namespace ImplementandoRedis.Application.Commands.TiposCerveja;

public sealed record CriarTipoCervejaCommand(string Nome, string Origem, string Coloracao, string TeorAlcoolico, string Fermentacao, string Descricao)
    : IRequest<CustomResult<TipoCervejaResponse>>;