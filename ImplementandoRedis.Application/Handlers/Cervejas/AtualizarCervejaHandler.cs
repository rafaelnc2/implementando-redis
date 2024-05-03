using ImplementandoRedis.Application.Commands.Cervejas;
using ImplementandoRedis.Shared.Responses.Cervejas;

namespace ImplementandoRedis.Application.Handlers.Cervejas;

public class AtualizarCervejaHandler : IRequestHandler<AtualizarCervejaCommand, CustomResult<CriarCervejaResponse>>
{
    private readonly ICervejaRepository _cervejaRepo;
    private readonly ITipoCervejaRepository _tipoCervejaRepo;

    public AtualizarCervejaHandler([FromKeyedServices(KeyedServicesName.CERVEJA_REDIS_REPO)] ICervejaRepository repository,
        [FromKeyedServices(KeyedServicesName.TIPO_CERVEJA_EF_REPO)] ITipoCervejaRepository tipoCervejaRepo)
    {
        _cervejaRepo = repository;
        _tipoCervejaRepo = tipoCervejaRepo;
    }

    public async Task<CustomResult<CriarCervejaResponse>> Handle(AtualizarCervejaCommand request, CancellationToken cancellationToken)
    {
        var response = new CustomResult<CriarCervejaResponse>();

        Guid requestId;
        if (Guid.TryParse(request.Id, out requestId) is false)
            return response.BadRequestResponse("ID informado é inválido");

        var cerveja = await _cervejaRepo.ObterPorIdAsync(requestId);

        if (cerveja is null)
            response.NotFoundResponse();

        var tipoCerveja = cerveja.TipoCerveja;

        if (request.TipoCervejaId != cerveja.TipoCervejaId)
        {
            tipoCerveja = await _tipoCervejaRepo.ObterPorIdAsync(request.TipoCervejaId);

            if (tipoCerveja is null)
                return response.BadRequestResponse("Tipo de cerveja informado não é válido");
        }

        cerveja.Update(
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

        await _cervejaRepo.AtualizarAsync(cerveja);

        CriarCervejaResponse cervejaResponse = cerveja;

        return response.OkResponse(cervejaResponse);
    }
}
