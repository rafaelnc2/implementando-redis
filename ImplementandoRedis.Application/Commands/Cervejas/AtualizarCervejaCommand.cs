﻿using ImplementandoRedis.Shared.Responses.Cervejas;

namespace ImplementandoRedis.Application.Commands.Cervejas;

public sealed record AtualizarCervejaCommand(string Nome, string Fabricante, bool Artesanal, int TipoCervejaId, string Descricao, string Harmonizacao, int AnoLancamento)
    : IRequest<CustomResult<CriarCervejaResponse>>
{
    public string Id { get; set; } = null!;

    public void SetCervejaId(string id)
    {
        Id = id;
    }
}
