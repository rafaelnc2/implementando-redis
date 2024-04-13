namespace ImplementandoRedis.Core.Interfaces;

public interface ISendMessageService
{
    public Task SendMessage(string queueName, string message);
}
