using System.Diagnostics;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Order.Application;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var jsonParameters = JsonSerializer.Serialize(request, new JsonSerializerOptions { WriteIndented = true });

        _logger.LogInformation($"{request.GetType()} is starting with parameters: {Environment.NewLine} {jsonParameters}");

        var timer = Stopwatch.StartNew();

        var response = await next();

        timer.Stop();

        _logger.LogInformation($"{request.GetType()} has finished in {timer.ElapsedMilliseconds} ms.");

        return response;
    }
}