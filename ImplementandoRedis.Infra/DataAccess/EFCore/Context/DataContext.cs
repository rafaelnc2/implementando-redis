using ImplementandoRedis.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CacheSample.Infra.DataAccess.EFCore.Context;

public class DataContext : DbContext
{
    private readonly IPublisher _eventPublisher;

    public DataContext(DbContextOptions<DataContext> options, IPublisher eventPublisher) : base(options)
    {
        _eventPublisher = eventPublisher;
    }

    public DbSet<Cerveja> Cerveja { get; set; }
    public DbSet<TipoCerveja> TipoCerveja { get; set; }



    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = ChangeTracker.Entries<Entity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .SelectMany(e => e.DomainEvents)
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await _eventPublisher.Publish(domainEvent, cancellationToken);
        }

        return result;
    }
}
