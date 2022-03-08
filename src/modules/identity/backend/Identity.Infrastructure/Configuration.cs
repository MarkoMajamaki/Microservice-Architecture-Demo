
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Identity.Domain;

namespace Identity.Infrastructure;

public static partial class ConfigurationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration["IDENTITY:CONNECTIONSTRING"];                
        System.Console.WriteLine("connectionString:" + connectionString);

        // For Entity Framework  
        services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Identity.Infrastructure")));        

        // For Identity  
        services.AddIdentity<ApplicationUser, IdentityRole>()  
            .AddEntityFrameworkStores<IdentityContext>()  
            .AddDefaultTokenProviders();  

        return services;
    }
}