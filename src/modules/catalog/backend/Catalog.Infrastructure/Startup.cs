using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application;
using Shared.Infrastructure;

namespace Catalog.Infrastructure;

public static partial class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IDateTimeService, DateTimeService>();
        services.AddTransient<IDomainEventService, DomainEventService>();

        services.AddTransient<IProductRepository, ProductRepository>();

        // Add SQL connection to database
        string connectionString = configuration["CONNECTIONSTRING"];              
        if (connectionString != null)
        {
        }  

        return services;
    }
}