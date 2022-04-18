
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Inventory.Infrastructure;

public class InventoryContextDesignTimeFactory : IDesignTimeDbContextFactory<InventoryContext>
{
    public InventoryContextDesignTimeFactory()
    {
        // A parameter-less constructor is required by the EF Core CLI tools.
    }

    public InventoryContext CreateDbContext(string[] args)
    {
        string connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRING") ?? "DUMMY_CONNECTION_STRING";
        
        var options = new DbContextOptionsBuilder<InventoryContext>()
            .UseSqlServer(connectionString, x => x.MigrationsAssembly("Inventory.Infrastructure"))
            .Options;

        return new InventoryContext(options, null, null, null);
    }
}