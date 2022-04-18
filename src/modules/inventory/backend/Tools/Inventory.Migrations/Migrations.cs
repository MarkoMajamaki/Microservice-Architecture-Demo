using Microsoft.EntityFrameworkCore;
using Inventory.Infrastructure;

namespace Inventory.Migrations;

public class Migrations
{        
    public static void Run(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<InventoryContext>();

        optionsBuilder.UseSqlServer(
            connectionString, 
            x => x.MigrationsAssembly("Inventory.Infrastructure"));

        using (var context = new InventoryContext(optionsBuilder.Options, null, null))
        {
            context.Database.Migrate();
        }
    } 
}
