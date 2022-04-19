using Shared.Domain;

namespace Order.Domain;

public class OrderCreatedEvent : DomainEvent
{
    public Order Order { get; init; }

    public OrderCreatedEvent(Order order)
    {
        Order = order;
    }
}