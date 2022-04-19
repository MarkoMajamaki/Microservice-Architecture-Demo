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

        return services;
    }
}