using CacheSample.Infra.DataAccess.EFCore.Context;
using ImplementandoRedis.Core.Entities;
using ImplementandoRedis.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ImplementandoRedis.Infra.Repositories.EFCore;

public class TipoCervejaEfRepository : ITipoCervejaRepository
{
    private readonly DataContext _ctx;

    public TipoCervejaEfRepository(DataContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<TipoCerveja> CriarAsync(TipoCerveja tipoCerveja)
    {
        await _ctx.TipoCerveja.AddAsync(tipoCerveja);

        await _ctx.SaveChangesAsync();

        return tipoCerveja;
    }

    public async Task<TipoCerveja> AtualizarAsync(TipoCerveja tipoCerveja)
    {
        _ctx.TipoCerveja.Update(tipoCerveja);

        await _ctx.SaveChangesAsync();

        return tipoCerveja;
    }

    public async Task<IEnumerable<TipoCerveja>> ObterTodosAsync() =>
        await _ctx.TipoCerveja
            .OrderBy(tipo => tipo.Nome)
            .ToListAsync();

    public async Task<TipoCerveja?> ObterPorIdAsync(int tipoCervejaId) =>
        await _ctx.TipoCerveja
            .FirstOrDefaultAsync(x => x.Id == tipoCervejaId);

    public async Task<TipoCerveja?> ObterPorIdNoTrackAsync(int tipoCervejaId) =>
        await _ctx.TipoCerveja
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == tipoCervejaId);

    //public Task<IEnumerable<TipoCerveja>> ObterPorFiltroAsync(string nome)
    //{
    //    throw new NotImplementedException();
    //}

    public Task<IEnumerable<TipoCerveja>> ObterPorFiltroAsync(Expression<Func<TipoCerveja, bool>> filter)
    {
        throw new NotImplementedException();
    }
}
