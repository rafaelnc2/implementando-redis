using ImplementandoRedis.Application.Commands.TiposCerveja;
using ImplementandoRedis.Application.Queries.TiposCerveja;

namespace ImplementandoRedis.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TipoCervejaController : ApiBaseController<TipoCervejaController>
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorIdAsync([FromRoute] int id)
    {
        Logger.LogInformation("Obter Tipo de Cerveja por ID");

        var query = new ObterTipoCervejaPorIdQuery(id);

        var result = await Mediator.Send(query);

        return ApiResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodosAsync()
    {
        Logger.LogInformation("Obter todos os Tipos de Cerveja cadastrados");

        var result = await Mediator.Send(new ObterTodosTiposCervejaQuery());

        return ApiResult(result);
    }

    [HttpGet("filtro")]
    public async Task<IActionResult> ObterPorNomeAsync([FromQuery] string nome, string origem, string coloracao, string teorAlcoolico, string fermentacao)
    {
        Logger.LogInformation("Obter Tipos de Cerveja por nome");

        var result = await Mediator.Send(new ObterTipoCervejaPorFiltrosQuery(nome, origem, coloracao, teorAlcoolico, fermentacao));

        return ApiResult(result);
    }


    [HttpPost]
    public async Task<IActionResult> CriarTipoCervejaAsync([FromBody] CriarTipoCervejaCommand novoTipoCerveja)
    {
        Logger.LogInformation("Criar Tipos de Cerveja cadastrados");

        var result = await Mediator.Send(novoTipoCerveja);

        return ApiResult(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> AtualizarTipoCervejaAsync([FromRoute] int id, [FromBody] AtualizarTipoCervejaCommand atualizarTipoCerveja)
    {
        Logger.LogInformation("Atualizar Tipo de Cerveja");

        atualizarTipoCerveja.SetTipoCervejaId(id);

        var result = await Mediator.Send(atualizarTipoCerveja);

        return ApiResult(result);
    }
}
