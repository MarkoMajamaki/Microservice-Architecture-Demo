
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Order.Infrastructure;

public class OrderContextDesignTimeFactory : IDesignTimeDbContextFactory<OrderContext>
{
    public OrderContextDesignTimeFactory()
    {
        // A parameter-less constructor is required by the EF Core CLI tools.
    }

    public OrderContext CreateDbContext(string[] args)
    {
        string connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRING") ?? "DUMMY_CONNECTION_STRING";
        
        var options = new DbContextOptionsBuilder<OrderContext>()
            .UseSqlServer(connectionString, x => x.MigrationsAssembly("Order.Infrastructure"))
            .Options;

        return new OrderContext(options, null, null);
    }
}