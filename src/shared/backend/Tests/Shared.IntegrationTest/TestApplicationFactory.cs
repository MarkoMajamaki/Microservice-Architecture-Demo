using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Shared.Tests;

public class TestApplicationFactory<TStartup, TDbContext>  : WebApplicationFactory<TStartup> 
    where TStartup: class
    where TDbContext: DbContext
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TDbContext>));
            services.Remove(descriptor);

            // Test with actual SQL database if connection string is configured, if not then use in memory database

            var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            string connectionString = config["CONNECTIONSTRING"];              
            if (connectionString != null)
            {
                System.Diagnostics.Debug.WriteLine($"Test with SQL server: {connectionString}");
                services.AddDbContext<TDbContext>(options => options.UseSqlServer(connectionString));        
            }  
            else 
            {
                System.Diagnostics.Debug.WriteLine($"Test with in memory database");
                services.AddDbContext<TDbContext>(options => options.UseInMemoryDatabase("InMemoryDbForTesting"));
            }

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<TDbContext>();
                db.Database.EnsureCreated();
            }
        });
    }
}
