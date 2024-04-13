using ImplementandoRedis.Application.Commands.Cervejas;
using ImplementandoRedis.Application.Events.Cervejas;
using ImplementandoRedis.Core.Repositories;
using ImplementandoRedis.Shared.Constants;
using ImplementandoRedis.Shared.Responses.Cervejas;
using Microsoft.Extensions.DependencyInjection;

namespace ImplementandoRedis.Application.Handlers.Cervejas;

public class CriarCervejaHandler : IRequestHandler<CriarCervejaCommand, CustomResult<CriarCervejaResponse>>
{
    private readonly ICervejaRepository _cervejaRepo;
    private readonly ITipoCervejaRepository _tipoCervejaRepo;
    private readonly IPublisher _eventPublisher;

    public CriarCervejaHandler([FromKeyedServices(KeyedServicesName.CERVEJA_REDIS_REPO)] ICervejaRepository repository,
        [FromKeyedServices(KeyedServicesName.TIPO_CERVEJA_EF_REPO)] ITipoCervejaRepository tipoCervejaRepo,
        IPublisher eventPublisher)
    {
        _cervejaRepo = repository;
        _tipoCervejaRepo = tipoCervejaRepo;
        _eventPublisher = eventPublisher;
    }

    public async Task<CustomResult<CriarCervejaResponse>> Handle(CriarCervejaCommand request, CancellationToken cancellationToken)
    {
        var response = new CustomResult<CriarCervejaResponse>();

        var tipoCerveja = await _tipoCervejaRepo.ObterPorIdNoTrackAsync(request.Tipo);

        if (tipoCerveja is null)
            return response.BadRequestResponse("Tipo de cerveja informado não é válido");

        var cerveja = new Cerveja(
            request.Nome,
            request.Fabricante,
            request.Artesanal,
            request.Descricao,
            request.AnoLancamento,
            tipoCerveja.Id,
            tipoCerveja
        );

        if (cerveja.IsValid is false)
            response.BadRequestResponse(cerveja.Errors);

        await _cervejaRepo.CriarAsync(cerveja);

        await _eventPublisher.Publish(new CriarCervejaEvent(cerveja), cancellationToken);

        CriarCervejaResponse cervejaResponse = cerveja;

        return response.CreatedResponse(cervejaResponse);
    }
}