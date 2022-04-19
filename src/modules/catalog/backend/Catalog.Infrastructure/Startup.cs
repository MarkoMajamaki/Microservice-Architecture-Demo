using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure;

public static partial class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services;
    }
}