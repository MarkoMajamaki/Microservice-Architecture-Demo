
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application;
using MassTransit;
using Microsoft.Extensions.Configuration;
using FluentValidation;

namespace Order.Application;

public static partial class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Order.Application.Startup));
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

        var rabbitMqSettings = configuration.GetSection(RabbitMQSettings.Key).Get<RabbitMQSettings>();

        if (rabbitMqSettings != null)
        {
            services.AddMassTransit(config => {

                RegisterIntegrationEvents(config);

                config.SetKebabCaseEndpointNameFormatter();

                config.AddSagaStateMachine<CreateOrderStateMachine, CreateOrderState>()
                    .InMemoryRepository(); // TODO

                config.UsingRabbitMq((ctx, cfg) => {

                    cfg.Host($"amqp://{rabbitMqSettings.UserName}:{rabbitMqSettings.Password}@{rabbitMqSettings.HostName}:{rabbitMqSettings.Port}");
                    
                    cfg.ConfigureEndpoints(ctx);
                });
            });
        }
        else
        {
            // TODO: Logging
        }

        return services;
    }

    private static void RegisterIntegrationEvents(IBusRegistrationConfigurator registerer)
    {
        registerer.AddConsumer<UserDeletedIntegrationEventHandler>();
    }
}