using Order.Infrastructure;
using Order.Domain;
using Microsoft.EntityFrameworkCore;
using Shared.Application;
using Shared.Infrastructure;

namespace Order.DbPopulator;
public class DbPopulator
{
    public static async Task RunAsync(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();

        optionsBuilder.UseSqlServer(
            connectionString, 
            x => x.MigrationsAssembly("Order.Infrastructure"));

        using (var db = new OrderContext(
            optionsBuilder.Options, null, null, new DateTimeService()))
        {
            await db.Database.EnsureDeletedAsync();
            await db.Database.EnsureCreatedAsync();
            
            Customer customer = new Customer("Tim", "Andersson", "United States", "New York", "10001", "21 W 33rd St");
            db.Customer.Add(customer);
            await db.SaveChangesAsync();

            Order.Domain.Order order = new Order.Domain.Order(customer.Id, new Address("Test", "Test", "0", "Test"));
            order.AddItem(new OrderItem(1, "Product name1", 1, null, 1));
            order.AddItem(new OrderItem(1, "Product name2", 1, null, 1));
            order.AddItem(new OrderItem(1, "Product name3", 1, null, 1));
            order.AddItem(new OrderItem(1, "Product name4", 1, null, 1));
            order.AddItem(new OrderItem(1, "Product name%", 1, null, 1));
            db.Orders.Add(order);
            await db.SaveChangesAsync();
        }
    } 
}