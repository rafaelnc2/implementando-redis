using ImplementandoRedis.Application.Queries.Cervejas;
using ImplementandoRedis.Shared.Responses.Cervejas;

namespace ImplementandoRedis.Application.Handlers.Cervejas;

public class ObterCervejaPorIdHandler : IRequestHandler<ObterCervejaPorIdQuery, CustomResult<ObterCervejaPorIdResponse>>
{
    private readonly ICervejaRepository _cervejaRepo;

    public ObterCervejaPorIdHandler([FromKeyedServices(KeyedServicesName.CERVEJA_REDIS_REPO)] ICervejaRepository cervejaRepo)
    {
        _cervejaRepo = cervejaRepo;
    }

    public async Task<CustomResult<ObterCervejaPorIdResponse>> Handle(ObterCervejaPorIdQuery request, CancellationToken cancellationToken)
    {
        var response = new CustomResult<ObterCervejaPorIdResponse>();

        var cerveja = await _cervejaRepo.ObterPorIdAsync(request.Id);

        if (cerveja is not Cerveja)
            return response.BadRequestResponse("Cerveja não encontrada");

        ObterCervejaPorIdResponse cervejaPorIdResponse = cerveja;

        return response.OkResponse(cervejaPorIdResponse);
    }
}
