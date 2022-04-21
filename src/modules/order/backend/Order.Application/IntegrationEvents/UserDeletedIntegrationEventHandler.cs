using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Application.IntegrationEvents;

namespace Order.Application;

public class UserDeletedIntegrationEventHandler : IConsumer<UserDeletedIntegrationEvent>
{
    private readonly ILogger<UserDeletedIntegrationEventHandler> _logger;

    public UserDeletedIntegrationEventHandler(ILogger<UserDeletedIntegrationEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<UserDeletedIntegrationEvent> context)
    {
        _logger.LogInformation("Handle user delete");

        return Task.CompletedTask;
    }
}