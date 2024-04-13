using ImplementandoRedis.Application;
using MediatR.NotificationPublishers;

namespace CacheSample.Api.Extensions;

public static class MediatrServiceExtensions
{
    public static void AddMediatrService(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<AssemblyRegister>();

            //config.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));

            config.NotificationPublisher = new ForeachAwaitPublisher();
        });
    }
}
