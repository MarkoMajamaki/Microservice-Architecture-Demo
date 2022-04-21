using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application;
using Shared.Infrastructure;

namespace Basket.Infrastructure;

public static partial class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IDateTimeService, DateTimeService>();
        services.AddTransient<IDomainEventService, DomainEventService>();
        return services;
    }
}