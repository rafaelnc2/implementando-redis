using ImplementandoRedis.Shared.Responses.TiposCerveja;

namespace ImplementandoRedis.Application.Handlers.TiposCerveja;

public class AtualizarTipoCervejaHandler : IRequestHandler<AtualizarTipoCervejaCommand, CustomResult<TipoCervejaResponse>>
{
    private readonly ITipoCervejaRepository _tipoCervejaRepo;

    public AtualizarTipoCervejaHandler([FromKeyedServices(KeyedServicesName.TIPO_CERVEJA_EF_REPO)] ITipoCervejaRepository tipoCervejaRepo)
    {
        _tipoCervejaRepo = tipoCervejaRepo;
    }

    public async Task<CustomResult<TipoCervejaResponse>> Handle(AtualizarTipoCervejaCommand request, CancellationToken cancellationToken)
    {
        var response = new CustomResult<TipoCervejaResponse>();

        var tipoCerveja = await _tipoCervejaRepo.ObterPorIdAsync(request.Id);

        if (tipoCerveja is not TipoCerveja)
            return response.NotFoundResponse();

        tipoCerveja.UpdateData(request.Nome, request.Origem, request.Coloracao, request.TeorAlcoolico, request.Fermentacao, request.Descricao);

        await _tipoCervejaRepo.AtualizarAsync(tipoCerveja);

        return response.OkResponse(tipoCerveja);
    }
}
