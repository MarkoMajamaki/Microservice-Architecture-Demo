using Shared.Domain;

namespace Order.Application;

public class OrderCreatedEvent : DomainEvent
{
    public Order.Domain.Order Order { get; init; }

    public OrderCreatedEvent(Order.Domain.Order order)
    {
        Order = order;
    }
}