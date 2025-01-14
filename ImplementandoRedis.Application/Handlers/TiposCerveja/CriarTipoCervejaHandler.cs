﻿using ImplementandoRedis.Application.Commands.TiposCerveja;
using ImplementandoRedis.Shared.Responses.TiposCerveja;

namespace ImplementandoRedis.Application.Handlers.TiposCerveja;

public class CriarTipoCervejaHandler : IRequestHandler<CriarTipoCervejaCommand, CustomResult<TipoCervejaResponse>>
{
    private readonly ITipoCervejaRepository _tipoCervejaRepo;

    public CriarTipoCervejaHandler([FromKeyedServices(KeyedServicesName.TIPO_CERVEJA_EF_REPO)] ITipoCervejaRepository tipoCervejaRepo)
    {
        _tipoCervejaRepo = tipoCervejaRepo;
    }

    public async Task<CustomResult<TipoCervejaResponse>> Handle(CriarTipoCervejaCommand request, CancellationToken cancellationToken)
    {
        var response = new CustomResult<TipoCervejaResponse>();

        var tipoCerveja = TipoCerveja.Create(
            request.Nome,
            request.Origem,
            request.Coloracao,
            request.TeorAlcoolico,
            request.Fermentacao,
            request.Descricao
        );

        if (tipoCerveja.IsValid is false)
            response.BadRequestResponse(tipoCerveja.Errors);

        await _tipoCervejaRepo.CriarAsync(tipoCerveja);

        TipoCervejaResponse tipoCervejaResponse = tipoCerveja;

        return response.CreatedResponse(tipoCervejaResponse);
    }
}

//https://www.sindicerv.com.br/tipos-de-cerveja/#:~:text=Tipos%20de%20cerveja-,As%20cervejas%20s%C3%A3o%20classificadas%20de%20acordo%20com%20seu%20teor%20de,tipos%20de%20cervejas%20no%20mundo.