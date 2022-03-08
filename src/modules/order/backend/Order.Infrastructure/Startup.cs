
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain;

namespace Order.Infrastructure;

public static partial class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IOrderRepository, OrderRepository>();

        // Add SQL connection to database
        string connectionString = configuration["CONNECTIONSTRING"];              
        if (connectionString != null)
        {
            services.AddDbContext<OrderContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Order.Infrastructure")));        
        }  

        return services;
    }
}