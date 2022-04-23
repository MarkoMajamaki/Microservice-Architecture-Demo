using System.Reflection;
using Microsoft.EntityFrameworkCore;

using Order.Domain;
using Shared.Application;
using Shared.Infrastructure;

namespace Order.Infrastructure;
public class OrderContext : DbContextBase
{
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Order.Domain.Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public OrderContext(
        DbContextOptions<OrderContext> options,
        IIdentityService identityService,
        IDomainEventService domainEventService,
        IDateTimeService dateTimeService) 
        : base(options, identityService, domainEventService, dateTimeService)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {   
        // Apply all entity configurations from this assembly
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}