using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using MassTransit;
using Shared.Application;
using Microsoft.Extensions.Configuration;

namespace Catalog.Application;

public static partial class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddMassTransit(config => {

            config.SetKebabCaseEndpointNameFormatter();

            config.UsingRabbitMq((ctx, cfg) => {

                var rabbitMqSettings = configuration.GetSection(RabbitMQSettings.Key).Get<RabbitMQSettings>();

                cfg.Host($"amqp://{rabbitMqSettings.UserName}:{rabbitMqSettings.Password}@{rabbitMqSettings.HostName}:{rabbitMqSettings.Port}");
            });
        });

        return services;
    }
}