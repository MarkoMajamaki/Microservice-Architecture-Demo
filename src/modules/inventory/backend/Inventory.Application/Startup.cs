using System.Reflection;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Application;

public static partial class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
     
        services.AddMassTransit(c => 
        {
            c.UsingRabbitMq((ctx, cfg) => 
            {
                cfg.Host("amqp://guest:guest@localhost:5672");
            });
        });

        return services;
    }
}

