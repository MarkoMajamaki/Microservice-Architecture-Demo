using Microsoft.EntityFrameworkCore;
using Order.Infrastructure;

namespace Order.Migrations;

public class Migrations
{        
    public static void Run(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();

        optionsBuilder.UseSqlServer(
            connectionString, 
            x => x.MigrationsAssembly("Order.Infrastructure"));

        using (var context = new OrderContext(optionsBuilder.Options, null, null))
        {
            context.Database.Migrate();
        }
    } 
}
