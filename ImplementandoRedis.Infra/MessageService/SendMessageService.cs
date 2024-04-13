using ImplementandoRedis.Core.Interfaces;
using StackExchange.Redis;

namespace ImplementandoRedis.MessageService.Services;

public class SendMessageService : ISendMessageService
{
    private readonly ISubscriber _subscriber;

    public SendMessageService(IConnectionMultiplexer redisMultiplexerConnect)
    {
        _subscriber = redisMultiplexerConnect.GetSubscriber();
    }

    public async Task SendMessage(string queueName, string message)
    {
        await _subscriber.PublishAsync(queueName, message);
    }
}
