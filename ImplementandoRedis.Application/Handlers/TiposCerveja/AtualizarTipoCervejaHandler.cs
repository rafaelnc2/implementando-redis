using ImplementandoRedis.Application.Events.TiposCerveja;
using ImplementandoRedis.Core.Repositories;
using ImplementandoRedis.Shared.Constants;
using ImplementandoRedis.Shared.Responses.TiposCerveja;
using Microsoft.Extensions.DependencyInjection;

namespace ImplementandoRedis.Application.Handlers.TiposCerveja;

public class AtualizarTipoCervejaHandler : IRequestHandler<AtualizarTipoCervejaCommand, CustomResult<TipoCervejaResponse>>
{
    private readonly ITipoCervejaRepository _tipoCervejaRepo;
    private readonly IPublisher _eventPublisher;

    public AtualizarTipoCervejaHandler([FromKeyedServices(KeyedServicesName.TIPO_CERVEJA_EF_REPO)] ITipoCervejaRepository tipoCervejaRepo, IPublisher eventPublisher)
    {
        _tipoCervejaRepo = tipoCervejaRepo;
        _eventPublisher = eventPublisher;
    }

    public async Task<CustomResult<TipoCervejaResponse>> Handle(AtualizarTipoCervejaCommand request, CancellationToken cancellationToken)
    {
        var response = new CustomResult<TipoCervejaResponse>();

        var tipoCerveja = await _tipoCervejaRepo.ObterPorIdAsync(request.Id);

        if (tipoCerveja is not TipoCerveja)
            return response.NotFoundResponse();

        tipoCerveja.UpdateData(request.Nome, request.Origem, request.Coloracao, request.TeorAlcoolico, request.Fermentacao, request.Descricao);

        await _tipoCervejaRepo.AtualizarAsync(tipoCerveja);

        await _eventPublisher.Publish(new AtualizarTipoCervejaEvent(tipoCerveja), cancellationToken);

        return response.OkResponse(tipoCerveja);
    }
}
