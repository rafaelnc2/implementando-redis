using ImplementandoRedis.Shared.Responses.TiposCerveja;

public sealed record AtualizarTipoCervejaCommand(string Nome, string Origem, string Coloracao, string TeorAlcoolico, string Fermentacao, string Descricao)
    : IRequest<CustomResult<TipoCervejaResponse>>
{
    public int Id { get; set; }

    public void SetTipoCervejaId(int id)
    {
        Id = id;
    }
}