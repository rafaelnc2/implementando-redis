using ImplementandoRedis.Core.Entities;
using ImplementandoRedis.Core.Repositories;
using Redis.OM;
using Redis.OM.Searching;
using StackExchange.Redis;
using System.Linq.Expressions;

namespace ImplementandoRedis.Infra.Repositories.Redis;

public class CervejaRedisRepository : ICervejaRepository
{
    private readonly RedisConnectionProvider _provider;
    private readonly RedisCollection<Cerveja> _cerveja;

    public CervejaRedisRepository(IConnectionMultiplexer redisMultiplexerConnect)
    {
        _provider = new RedisConnectionProvider(redisMultiplexerConnect);
        _cerveja = (RedisCollection<Cerveja>)_provider.RedisCollection<Cerveja>();
    }


    public async Task<Cerveja> CriarAsync(Cerveja cerveja)
    {
        await _cerveja.InsertAsync(cerveja);

        return cerveja;
    }


    public Task<Cerveja> AtualizarAsync(Cerveja cerveja)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Cerveja>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Cerveja?> ObterPorIdAsync(Guid cervejaId)
    {
        var cerveja = await _cerveja.FindByIdAsync(cervejaId.ToString());

        return cerveja;
    }

    public Task<Cerveja?> ObterPorIdNoTrackAsync(Guid cervejaId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Cerveja>> ObterPorFiltrosAsync(Expression<Func<Cerveja, bool>> filter) =>
        await _cerveja
            .Where(filter)
            .OrderBy(tipo => tipo.Nome)
            .ToListAsync();
}
