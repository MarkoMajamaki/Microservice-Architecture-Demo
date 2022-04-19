using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Application;

public static partial class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddMassTransit(config => {

            config.AddConsumer<ProductUpdatedIntegrationEventHandler>();

            config.UsingRabbitMq((ctx, cfg) => {
                cfg.Host("amqp://guest:guest@localhost:5672");
                
                cfg.ReceiveEndpoint("queue", c => {
                    c.ConfigureConsumer<ProductUpdatedIntegrationEventHandler>(ctx);
                });
            });
        });

        return services;
    }
}