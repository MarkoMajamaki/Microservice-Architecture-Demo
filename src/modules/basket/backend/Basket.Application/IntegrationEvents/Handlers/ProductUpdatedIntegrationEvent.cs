
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Basket.Application;

public class ProductUpdatedIntegrationEventHandler : IConsumer<ProductUpdatedIntegrationEvent>
{
    private readonly ILogger<ProductUpdatedIntegrationEventHandler> _logger;

    public ProductUpdatedIntegrationEventHandler(ILogger<ProductUpdatedIntegrationEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<ProductUpdatedIntegrationEvent> context)
    {
        _logger.LogInformation("Handle product update integration event");

        return Task.CompletedTask;
    }
}