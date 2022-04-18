using MediatR;
using Shared.Application;
using Shared.Domain;

namespace Shared.Infrastructure;

public class DomainEventService : IDomainEventService
{
    private readonly IPublisher _mediator;

    public DomainEventService(IPublisher mediator)
    {
        _mediator = mediator;
    }

    public async Task Publish(DomainEvent domainEvent)
    {
        // Send a notification to multiple event handlers using mediator
        await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
    }

    private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
    {
        // Create type of DomainEventNotification where generic type is same as domainEvent type
        var type = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
        
        // Create instace of that type 
        return (INotification)Activator.CreateInstance(type, domainEvent)!;
    }
}