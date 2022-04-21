using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain;
using Shared.Application;
using Shared.Infrastructure;

namespace Order.Infrastructure;

public static partial class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IDateTimeService, DateTimeService>();
        services.AddTransient<IDomainEventService, DomainEventService>();

        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<ICustomerRepository, CustomerRepository>();

        // Add SQL connection to database
        string connectionString = configuration["CONNECTIONSTRING"];              
        if (connectionString != null)
        {
            services.AddDbContext<OrderContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Order.Infrastructure")));        
        }  

        return services;
    }
}