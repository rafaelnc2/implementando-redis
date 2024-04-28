using ImplementandoRedis.Application.Queries.TiposCerveja;
using ImplementandoRedis.Shared.Responses.TiposCerveja;

namespace ImplementandoRedis.Application.Handlers.TiposCerveja;

public class ObterTodosTiposCervejaHandler : IRequestHandler<ObterTodosTiposCervejaQuery, CustomResult<IEnumerable<ObterTodosTiposCervejaResponse>>>
{
    private readonly ITipoCervejaRepository _tipoCervejaRepo;

    public ObterTodosTiposCervejaHandler([FromKeyedServices(KeyedServicesName.TIPO_CERVEJA_REDIS_REPO)] ITipoCervejaRepository tipoCervejaRepo)
    {
        _tipoCervejaRepo = tipoCervejaRepo;
    }

    public async Task<CustomResult<IEnumerable<ObterTodosTiposCervejaResponse>>> Handle(ObterTodosTiposCervejaQuery request, CancellationToken cancellationToken)
    {
        var response = new CustomResult<IEnumerable<ObterTodosTiposCervejaResponse>>();

        var tiposCerveja = await _tipoCervejaRepo.ObterTodosAsync();

        if (tiposCerveja is null)
            return response.OkResponse(Enumerable.Empty<ObterTodosTiposCervejaResponse>());

        var tiposCervejaResponse = tiposCerveja.ToList().ConvertAll<ObterTodosTiposCervejaResponse>(tipo => tipo);

        return response.OkResponse(tiposCervejaResponse);
    }
}
