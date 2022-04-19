using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using MassTransit;

namespace Catalog.Application;

public static partial class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddMassTransit(config => {

            config.AddConsumer<ProductCreatedIntegrationEventHandler>();

            config.UsingRabbitMq((ctx, cfg) => {
                cfg.Host("amqp://guest:guest@localhost:5672");
                
                cfg.ReceiveEndpoint("queue", c => {
                    c.ConfigureConsumer<ProductCreatedIntegrationEventHandler>(ctx);
                });
            });
        });

        return services;
    }
}