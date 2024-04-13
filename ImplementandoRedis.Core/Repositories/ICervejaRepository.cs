using ImplementandoRedis.Core.Entities;
using System.Linq.Expressions;

namespace ImplementandoRedis.Core.Repositories;

public interface ICervejaRepository
{
    public Task<Cerveja> CriarAsync(Cerveja cerveja);
    public Task<Cerveja> AtualizarAsync(Cerveja cerveja);



    Task<Cerveja?> ObterPorIdAsync(Guid cervejaId);
    Task<Cerveja?> ObterPorIdNoTrackAsync(Guid cervejaId);
    Task<IEnumerable<Cerveja>> GetAsync();
    Task<IEnumerable<Cerveja>> ObterPorFiltrosAsync(Expression<Func<Cerveja, bool>> filter);
}
