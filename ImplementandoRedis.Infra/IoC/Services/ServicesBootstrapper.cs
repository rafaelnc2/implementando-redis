using ImplementandoRedis.Core.Interfaces;
using ImplementandoRedis.MessageService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ImplementandoRedis.Infra.IoC.Services;

public class ServicesBootstrapper
{
    public void ServicesServiceRegister(IServiceCollection services)
    {
        services.AddTransient<ISendMessageService, SendMessageService>();
    }
}
