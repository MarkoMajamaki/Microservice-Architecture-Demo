using Microsoft.EntityFrameworkCore;
using Identity.Infrastructure;

namespace Identity.Migrations;

public class Migrations
{        
    public static void Run(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();

        optionsBuilder.UseSqlServer(
            connectionString, 
            x => x.MigrationsAssembly("Identity.Infrastructure"));

        using (var context = new IdentityContext(optionsBuilder.Options))
        {
            context.Database.Migrate();
        }
    } 
}
