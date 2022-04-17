using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application;

namespace Inventory.Infrastructure;

public static partial class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        services.AddTransient<IProductRepository, ProductRepository>();

        // Add SQL connection to database
        string connectionString = configuration["CONNECTIONSTRING"];              
        if (connectionString != null)
        {
            // services.AddDbContext<InventoryContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Inventory.Infrastructure")));        
        }  

        return services;
    }
}