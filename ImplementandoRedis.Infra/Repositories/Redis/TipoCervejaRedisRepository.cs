using ImplementandoRedis.Core.Filters;

namespace ImplementandoRedis.Infra.Repositories.Redis;

public class TipoCervejaRedisRepository : ITipoCervejaRepository
{
    private readonly RedisConnectionProvider _provider;
    private readonly RedisCollection<TipoCerveja> _tipoCerveja;
    private readonly IDatabase _database;

    private const string CERVEJA_IDX = "cerveja-idx";

    public TipoCervejaRedisRepository(IConnectionMultiplexer redisMultiplexerConnect)
    {
        _database = redisMultiplexerConnect.GetDatabase();
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

    public async Task<IEnumerable<TipoCerveja>> ObterPorFiltroAsync(ObterTipoCervejaFilters filters)
    {
        var parameters = new List<string>();

        if (filters is null)
            return Enumerable.Empty<TipoCerveja>();

        if (string.IsNullOrWhiteSpace(filters.Nome) is false)
            parameters.Add($"$nome:{filters.Nome}");

        if (string.IsNullOrWhiteSpace(filters.Origem) is false)
            parameters.Add($"$origem:{filters.Origem}");

        if (string.IsNullOrWhiteSpace(filters.Coloracao) is false)
            parameters.Add($"$coloracao:{filters.Coloracao}");

        if (string.IsNullOrWhiteSpace(filters.TeorAlcoolico) is false)
            parameters.Add($"teorAlcoolico:{filters.TeorAlcoolico}");

        if (string.IsNullOrWhiteSpace(filters.Fermentacao) is false)
            parameters.Add($"fermentacao:{filters.Fermentacao}");

        var result = await _provider.Connection.ExecuteAsync("FT.SEARCH", CERVEJA_IDX, parameters.ToArray());
        _database.

        return Enumerable.Empty<TipoCerveja>();
    }
}
