using System.Reflection;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore;
using Shared.Application;
using Shared.Infrastructure;

namespace Inventory.Infrastructure;

public class InventoryContext : DbContextBase
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Vendor> Vendors { get; set; }

    public InventoryContext(
        DbContextOptions<InventoryContext> options,
        IIdentityService identityService, 
        IDomainEventService domainEventService,
        IDateTimeService dateTimeService) 
    : base(options, identityService, domainEventService, dateTimeService)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {   
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}