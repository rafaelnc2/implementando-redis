using Microsoft.EntityFrameworkCore;

namespace CacheSample.Infra.DataAccess.EFCore.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Cerveja> Cerveja { get; set; }
    public DbSet<TipoCerveja> TipoCerveja { get; set; }
}
