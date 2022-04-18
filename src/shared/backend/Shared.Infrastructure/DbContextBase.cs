using System.Reflection;
using Microsoft.EntityFrameworkCore;

using Shared.Application;
using Shared.Domain;

namespace Shared.Infrastructure;

public class DbContextBase : DbContext
{
    private readonly IIdentityService _identityService;
    private readonly IDomainEventService _domainEventService;
    private readonly IDateTimeService _dateTimeService;
    
    public DbContextBase(
        DbContextOptions options,
        IIdentityService identityService,
        IDomainEventService domainEventService,
        IDateTimeService dateTimeService) : base(options)
    {
        _identityService = identityService;
        _domainEventService = domainEventService;
        _dateTimeService = dateTimeService;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {      
        // Add auditing values for debugging to all entities
        foreach (var entry in ChangeTracker.Entries<Entity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.SetCreatedBy(_identityService?.GetUserId());
                    entry.Entity.SetCreated(_dateTimeService?.Now);
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdateLastModifiedBy(_identityService?.GetUserId());
                    entry.Entity.UpdateLastModified(_dateTimeService?.Now);
                    break;
            }
        }

        // Get all domain entities for entries which is going to be saved to db
        var domainEvents = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .ToArray();
        
        // Publish domain events
        foreach (var domainEvent in domainEvents)
        {
            domainEvent.IsPublished = true;
            await _domainEventService?.Publish(domainEvent);
        }

        // Save chanes
        var result = await base.SaveChangesAsync(cancellationToken);
    
        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {   
        // Apply all entity configurations from this assembly
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}