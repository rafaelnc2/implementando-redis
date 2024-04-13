using Microsoft.Extensions.Logging;

namespace ImplementandoRedis.Application.Behaviors;

public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Iniciando request - {@RequestName}, {@Datetime}",
            typeof(TRequest).Name,
            DateTime.Now
        );

        var result = await next();

        if (result is false)
        {
            _logger.LogError(
                "Erro na requisição - {@RequestName}, {@Datetime}",
                typeof(TRequest).Name,
                DateTime.Now
            );
        }

        _logger.LogInformation(
            "Request completo - {@RequestName}, {@Datetime}",
            typeof(TRequest).Name,
            DateTime.Now
        );

        return result;
    }
}
