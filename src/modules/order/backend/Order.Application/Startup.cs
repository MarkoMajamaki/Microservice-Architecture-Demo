
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace Order.Application;

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
        registerer.AddConsumer<UserDeletedIntegrationEventHandler>();
    }
}