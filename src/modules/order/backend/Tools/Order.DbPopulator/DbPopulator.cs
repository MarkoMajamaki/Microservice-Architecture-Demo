using Order.Infrastructure;
using Order.Domain;
using Microsoft.EntityFrameworkCore;
using Shared.Application;

namespace Order.DbPopulator;
public class DbPopulator
{
    public static async Task RunAsync(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();

        optionsBuilder.UseSqlServer(
            connectionString, 
            x => x.MigrationsAssembly("Order.Infrastructure"));

        using (var db = new OrderContext(optionsBuilder.Options, null, new DateTimeService()))
        {
            await db.Database.EnsureDeletedAsync();
            await db.Database.EnsureCreatedAsync();
            
            Customer customer = new Customer("Tim", "Andersson", "United States", "New York", "10001", "21 W 33rd St");
            db.Customer.Add(customer);
            await db.SaveChangesAsync();

            Order.Domain.Order order = new Order.Domain.Order(customer.Id);
            order.AddItem(new OrderItem(1, 1));
            order.AddItem(new OrderItem(1, 2));
            order.AddItem(new OrderItem(1, 3));
            order.AddItem(new OrderItem(1, 4));
            db.Orders.Add(order);
            await db.SaveChangesAsync();
        }
    } 
}