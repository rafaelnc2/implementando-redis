using ImplementandoRedis.Application.Commands.Cervejas;
using ImplementandoRedis.Application.Queries.Cervejas;

namespace ImplementandoRedis.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CervejaController : ApiBaseController<CervejaController>
{
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> ObterPorIdAsync([FromRoute] Guid id)
    {
        Logger.LogInformation($"Obter cerveja por ID: {id}");

        var query = new ObterCervejaPorIdQuery(id);

        var result = await Mediator.Send(query);

        return ApiResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromBody] CriarCervejaCommand criarCommand)
    {
        Logger.LogInformation("Nova cerveja");

        var result = await Mediator.Send(criarCommand);

        return ApiResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarAsync([FromRoute] string id, [FromBody] AtualizarCervejaCommand atualizaCommand)
    {
        Logger.LogInformation($"Update student with ID = {id}");

        atualizaCommand.SetCervejaId(id);

        var result = await Mediator.Send(atualizaCommand);

        return ApiResult(result);
    }

    //[HttpGet]
    //public async Task<IActionResult> GetAll([FromQuery] GetAllStudentsQuery query, CancellationToken cancellationToken)
    //{
    //    Logger.LogInformation($"Try to get all students");

    //    var result = await Mediator.Send(query, cancellationToken);

    //    return ApiResult(result);
    //}
}
