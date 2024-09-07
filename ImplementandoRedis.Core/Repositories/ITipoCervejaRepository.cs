using ImplementandoRedis.Core.Entities;
using ImplementandoRedis.Core.Filters;
using System.Linq.Expressions;

namespace ImplementandoRedis.Core.Repositories;

public interface ITipoCervejaRepository
{
    public Task<TipoCerveja> CriarAsync(TipoCerveja tipoCerveja);
    public Task<TipoCerveja> AtualizarAsync(TipoCerveja tipoCerveja);



    Task<TipoCerveja?> ObterPorIdAsync(int tipoCervejaId);
    Task<TipoCerveja?> ObterPorIdNoTrackAsync(int tipoCervejaId);
    Task<IEnumerable<TipoCerveja>> ObterTodosAsync();
    Task<IEnumerable<TipoCerveja>> ObterPorFiltroAsync(Expression<Func<TipoCerveja, bool>> filter);
    Task<IEnumerable<TipoCerveja>> ObterPorFiltroAsync(ObterTipoCervejaFilters filters);
}
