using Inventory.Infrastructure;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore;
using Shared.Application;

namespace Inventory.DbPopulator;
public class DbPopulator
{
    public static async Task RunAsync(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<InventoryContext>();

        optionsBuilder.UseSqlServer(
            connectionString, 
            x => x.MigrationsAssembly("Inventory.Infrastructure"));

        using (var db = new InventoryContext(optionsBuilder.Options, null, new DateTimeService()))
        {
        }
    } 
}