using ImplementandoRedis.Shared.Responses.Cervejas;

namespace ImplementandoRedis.Application.Commands.Cervejas;

public sealed record CriarCervejaCommand(string Nome, string Fabricante, bool Artesanal, int TipoCervejaId, string Descricao, string Harmonizacao, int AnoLancamento)
    : IRequest<CustomResult<CriarCervejaResponse>>;
