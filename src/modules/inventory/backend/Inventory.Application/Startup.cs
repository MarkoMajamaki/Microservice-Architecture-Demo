using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application;

namespace Inventory.Application;

public static partial class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IDateTimeService, DateTimeService>();
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }
}

