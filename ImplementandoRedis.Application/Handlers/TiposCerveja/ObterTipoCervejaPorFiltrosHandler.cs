using ImplementandoRedis.Application.Queries.TiposCerveja;
using ImplementandoRedis.Core.Filters;
using ImplementandoRedis.Shared.Responses.TiposCerveja;

namespace ImplementandoRedis.Application.Handlers.TiposCerveja;

public class ObterTipoCervejaPorFiltrosHandler : IRequestHandler<ObterTipoCervejaPorFiltrosQuery, CustomResult<IEnumerable<TipoCervejaResponse>>>
{
    private readonly ITipoCervejaRepository _tipoCervejaRepo;

    public ObterTipoCervejaPorFiltrosHandler([FromKeyedServices(KeyedServicesName.TIPO_CERVEJA_REDIS_REPO)] ITipoCervejaRepository tipoCervejaRepo)
    {
        _tipoCervejaRepo = tipoCervejaRepo;
    }

    public async Task<CustomResult<IEnumerable<TipoCervejaResponse>>> Handle(ObterTipoCervejaPorFiltrosQuery request, CancellationToken cancellationToken)
    {
        var response = new CustomResult<IEnumerable<TipoCervejaResponse>>();

        //Expression<Func<TipoCerveja, bool>> filter = tipo => tipo.Nome.Contains(request.Nome);

        //https://code-maze.com/dynamic-queries-expression-trees-csharp/
        //var teste = CreateFilterExpression("nome", request.Nome);

        var filters = new ObterTipoCervejaFilters(request.Nome, request.Origem, request.Coloracao, request.TeorAlcoolico, request.Fermentacao);

        var tiposCerveja = await _tipoCervejaRepo.ObterPorFiltroAsync(filters);

        if (tiposCerveja is null)
            return response.OkResponse(Enumerable.Empty<TipoCervejaResponse>());

        var tiposCervejaResponse = tiposCerveja.ToList().ConvertAll<TipoCervejaResponse>(tipo => tipo);

        return response.OkResponse(tiposCervejaResponse);
    }


    //private Expression<Func<TipoCerveja, bool>> CreateFilterExpression(string propertyName, string propertyValue)
    //{
    //    //verificar melhor forma de fazer o filtro
    //    var parameterExp = Expression.Parameter(typeof(TipoCerveja), "type");

    //    var propertyExp = Expression.Property(parameterExp, propertyName);

    //    MethodInfo method = typeof(string).GetMethod("Contains", [typeof(string)]);

    //    var someValue = Expression.Constant(propertyValue, typeof(string));

    //    var containsMethodExp = Expression.Call(propertyExp, method, someValue);

    //    return Expression.Lambda<Func<TipoCerveja, bool>>(containsMethodExp, parameterExp);
    //}
}
