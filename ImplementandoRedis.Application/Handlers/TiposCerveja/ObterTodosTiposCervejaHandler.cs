using ImplementandoRedis.Application.Queries.TiposCerveja;
using ImplementandoRedis.Shared.Responses.TiposCerveja;

namespace ImplementandoRedis.Application.Handlers.TiposCerveja;

public class ObterTodosTiposCervejaHandler : IRequestHandler<ObterTodosTiposCervejaQuery, CustomResult<IEnumerable<TipoCervejaResponse>>>
{
    private readonly ITipoCervejaRepository _tipoCervejaRepo;

    public ObterTodosTiposCervejaHandler([FromKeyedServices(KeyedServicesName.TIPO_CERVEJA_REDIS_REPO)] ITipoCervejaRepository tipoCervejaRepo)
    {
        _tipoCervejaRepo = tipoCervejaRepo;
    }

    public async Task<CustomResult<IEnumerable<TipoCervejaResponse>>> Handle(ObterTodosTiposCervejaQuery request, CancellationToken cancellationToken)
    {
        var response = new CustomResult<IEnumerable<TipoCervejaResponse>>();

        var tiposCerveja = await _tipoCervejaRepo.ObterTodosAsync();

        if (tiposCerveja is null)
            return response.OkResponse(Enumerable.Empty<TipoCervejaResponse>());

        var tiposCervejaResponse = tiposCerveja.ToList().ConvertAll<TipoCervejaResponse>(tipo => tipo);

        return response.OkResponse(tiposCervejaResponse);
    }
}
