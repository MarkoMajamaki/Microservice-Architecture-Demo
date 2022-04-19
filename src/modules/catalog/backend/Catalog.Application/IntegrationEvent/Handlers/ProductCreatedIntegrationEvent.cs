namespace Catalog.Application;

using MassTransit;
using Microsoft.Extensions.Logging;

public class ProductCreatedIntegrationEventHandler : IConsumer<ProductCreatedIntegrationEvent>
{
    private readonly ILogger<ProductCreatedIntegrationEventHandler> _logger;

    public ProductCreatedIntegrationEventHandler(ILogger<ProductCreatedIntegrationEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<ProductCreatedIntegrationEvent> context)
    {
        _logger.LogInformation("Handle product created integration event");

        return Task.CompletedTask;
    }
}