using System.Reflection;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application;

namespace Basket.Application;

public static partial class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddMassTransit(config => {

            RegisterIntegrationEvents(config);

            config.SetKebabCaseEndpointNameFormatter();

            config.UsingRabbitMq((ctx, cfg) => {

                var rabbitMqSettings = configuration.GetSection(RabbitMQSettings.Key).Get<RabbitMQSettings>();

                cfg.Host($"amqp://{rabbitMqSettings.UserName}:{rabbitMqSettings.Password}@{rabbitMqSettings.HostName}:{rabbitMqSettings.Port}");
                
                cfg.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }

    private static void RegisterIntegrationEvents(IBusRegistrationConfigurator registerer)
    {
            registerer.AddConsumer<ProductUpdatedIntegrationEventHandler>();
    }
}