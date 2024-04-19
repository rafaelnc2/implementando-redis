using CacheSample.Infra.DataAccess.EFCore.Context;
using ImplementandoRedis.Core.Entities;
using ImplementandoRedis.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ImplementandoRedis.Infra.Repositories.EFCore;

public class CervejaEfRepository : ICervejaRepository
{
    private readonly DataContext _ctx;

    public CervejaEfRepository(DataContext ctx)
    {
        _ctx = ctx;
    }

    public Task<Cerveja> AtualizarAsync(Cerveja cerveja)
    {
        throw new NotImplementedException();
    }

    public async Task<Cerveja> CriarAsync(Cerveja cerveja)
    {
        await _ctx.Set<Cerveja>().AddAsync(cerveja);

        //Ignora a propriedade Tipo na hora de incluir / evita o ef querer incluir um novo tipo
        _ctx.Entry(cerveja.Tipo).State = EntityState.Unchanged;

        await _ctx.SaveChangesAsync();

        return cerveja;
    }

    public Task<IEnumerable<Cerveja>> GetAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Cerveja?> ObterPorIdAsync(Guid cervejaId)
    {
        var cerveja = await _ctx.Cerveja
            .Include(t => t.Tipo)
            .FirstOrDefaultAsync(c => c.Id == cervejaId);

        return cerveja;
    }

    public async Task<Cerveja?> ObterPorIdNoTrackAsync(Guid cervejaId)
    {
        var cerveja = await _ctx.Cerveja
            .AsNoTracking()
            .Include(t => t.Tipo)
            .FirstOrDefaultAsync(c => c.Id == cervejaId);

        return cerveja;
    }

    public Task<IEnumerable<Cerveja>> ObterPorFiltrosAsync(Expression<Func<Cerveja, bool>> filter)
    {
        throw new NotImplementedException();
    }
}