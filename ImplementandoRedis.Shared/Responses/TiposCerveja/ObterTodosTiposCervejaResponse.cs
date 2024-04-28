using ImplementandoRedis.Core.Entities;

namespace ImplementandoRedis.Shared.Responses.TiposCerveja;

public sealed record ObterTodosTiposCervejaResponse(int Id, string Nome)
{
    public static implicit operator ObterTodosTiposCervejaResponse(TipoCerveja tipoCerveja) =>
        new ObterTodosTiposCervejaResponse(
            Id: tipoCerveja.Id,
            Nome: tipoCerveja.Nome
        );
}
