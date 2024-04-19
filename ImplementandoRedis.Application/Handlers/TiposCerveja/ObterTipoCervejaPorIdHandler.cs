using ImplementandoRedis.Application.Queries.TiposCerveja;
using ImplementandoRedis.Shared.Responses.TiposCerveja;

namespace ImplementandoRedis.Application.Handlers.TiposCerveja;

public class ObterTipoCervejaPorIdHandler : IRequestHandler<ObterTipoCervejaPorIdQuery, CustomResult<TipoCervejaResponse>>
{
    private readonly ITipoCervejaRepository _tipoCervejaRepo;

    public ObterTipoCervejaPorIdHandler([FromKeyedServices(KeyedServicesName.TIPO_CERVEJA_REDIS_REPO)] ITipoCervejaRepository tipoCervejaRepo)
    {
        _tipoCervejaRepo = tipoCervejaRepo;
    }

    public async Task<CustomResult<TipoCervejaResponse>> Handle(ObterTipoCervejaPorIdQuery request, CancellationToken cancellationToken)
    {
        var response = new CustomResult<TipoCervejaResponse>();

        if (request.Id == 0)
            return response.BadRequestResponse("Id informado é inválido");

        var tipoCerveja = await _tipoCervejaRepo.ObterPorIdNoTrackAsync(request.Id);

        if (tipoCerveja is not TipoCerveja)
            return response.NotFoundResponse();

        TipoCervejaResponse tipoCervejaResponse = tipoCerveja;

        return response.OkResponse(tipoCervejaResponse);
    }
}
