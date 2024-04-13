using ImplementandoRedis.Application.Queries.TiposCerveja;
using ImplementandoRedis.Core.Repositories;
using ImplementandoRedis.Shared.Constants;
using ImplementandoRedis.Shared.Responses.TiposCerveja;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace ImplementandoRedis.Application.Handlers.TiposCerveja;

public class ObterTipoCervejaPorFiltrosHandler : IRequestHandler<ObterTipoCervejaPorFiltrosQuery, CustomResult<IEnumerable<TipoCervejaResponse>>>
{
    private readonly ITipoCervejaRepository _tipoCervejaRepo;

    public ObterTipoCervejaPorFiltrosHandler([FromKeyedServices(KeyedServicesName.TIPO_CERVEJA_REDIS_REPO)] ITipoCervejaRepository tipoCervejaRepo)
    {
        _tipoCervejaRepo = tipoCervejaRepo;
    }

    public async Task<CustomResult<IEnumerable<TipoCervejaResponse>>> Handle(ObterTipoCervejaPorFiltrosQuery request, CancellationToken cancellationToken)
    {
        var response = new CustomResult<IEnumerable<TipoCervejaResponse>>();

        Expression<Func<TipoCerveja, bool>> filter = tipo => tipo.Nome.Contains(request.Nome);

        var tiposCerveja = await _tipoCervejaRepo.ObterPorFiltroAsync(filter);

        if (tiposCerveja is null)
            return response.OkResponse(Enumerable.Empty<TipoCervejaResponse>());

        var tiposCervejaResponse = tiposCerveja.ToList().ConvertAll<TipoCervejaResponse>(tipo => tipo);

        return response.OkResponse(tiposCervejaResponse);
    }
}
