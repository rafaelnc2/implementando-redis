using ImplementandoRedis.Core.Entities;
using ImplementandoRedis.Core.Repositories;
using Redis.OM;
using Redis.OM.Searching;
using StackExchange.Redis;
using System.Linq.Expressions;

namespace ImplementandoRedis.Infra.Repositories.Redis;

public class TipoCervejaRedisRepository : ITipoCervejaRepository
{
    private readonly RedisConnectionProvider _provider;
    private readonly RedisCollection<TipoCerveja> _tipoCerveja;

    public TipoCervejaRedisRepository(IConnectionMultiplexer redisMultiplexerConnect)
    {
        _provider = new RedisConnectionProvider(redisMultiplexerConnect);
        _tipoCerveja = (RedisCollection<TipoCerveja>)_provider.RedisCollection<TipoCerveja>();
    }

    public async Task<TipoCerveja> AtualizarAsync(TipoCerveja tipoCerveja)
    {
        await _tipoCerveja.UpdateAsync(tipoCerveja);

        return tipoCerveja;
    }

    public async Task<TipoCerveja> CriarAsync(TipoCerveja tipoCerveja)
    {
        await _tipoCerveja.InsertAsync(tipoCerveja);

        return tipoCerveja;
    }

    public async Task<TipoCerveja?> ObterPorIdAsync(int tipoCervejaId) =>
        await _tipoCerveja.FindByIdAsync(tipoCervejaId.ToString());

    public async Task<TipoCerveja?> ObterPorIdNoTrackAsync(int tipoCervejaId) =>
        await ObterPorIdAsync(tipoCervejaId);

    public async Task<IEnumerable<TipoCerveja>> ObterTodosAsync() =>
        await _tipoCerveja
            .OrderBy(tipo => tipo.Nome)
            .ToListAsync();

    public async Task<IEnumerable<TipoCerveja>> ObterPorFiltroAsync(Expression<Func<TipoCerveja, bool>> filter) =>
        await _tipoCerveja
            .Where(filter)
            .OrderBy(tipo => tipo.Nome)
            .ToListAsync();
}
