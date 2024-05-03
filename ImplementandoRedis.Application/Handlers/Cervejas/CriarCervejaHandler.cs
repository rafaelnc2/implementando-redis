using ImplementandoRedis.Application.Commands.Cervejas;
using ImplementandoRedis.Shared.Responses.Cervejas;

namespace ImplementandoRedis.Application.Handlers.Cervejas;

public class CriarCervejaHandler : IRequestHandler<CriarCervejaCommand, CustomResult<CriarCervejaResponse>>
{
    private readonly ICervejaRepository _cervejaRepo;
    private readonly ITipoCervejaRepository _tipoCervejaRepo;

    public CriarCervejaHandler([FromKeyedServices(KeyedServicesName.CERVEJA_REDIS_REPO)] ICervejaRepository repository,
        [FromKeyedServices(KeyedServicesName.TIPO_CERVEJA_REDIS_REPO)] ITipoCervejaRepository tipoCervejaRepo)
    {
        _cervejaRepo = repository;
        _tipoCervejaRepo = tipoCervejaRepo;
    }

    public async Task<CustomResult<CriarCervejaResponse>> Handle(CriarCervejaCommand request, CancellationToken cancellationToken)
    {
        var response = new CustomResult<CriarCervejaResponse>();

        var tipoCerveja = await _tipoCervejaRepo.ObterPorIdAsync(request.TipoCervejaId);

        if (tipoCerveja is null)
            return response.BadRequestResponse("Tipo de cerveja informado não é válido");

        var cerveja = Cerveja.Create(
            request.Nome,
            request.Fabricante,
            request.Artesanal,
            request.Descricao,
            request.Harmonizacao,
            request.AnoLancamento,
            tipoCerveja
        );

        if (cerveja.IsValid is false)
            response.BadRequestResponse(cerveja.Errors);

        await _cervejaRepo.CriarAsync(cerveja);

        CriarCervejaResponse cervejaResponse = cerveja;

        return response.CreatedResponse(cervejaResponse);
    }
}