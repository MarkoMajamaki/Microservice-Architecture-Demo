using Shared.Domain;

namespace Order.Domain;

public class OrderCreatedDomainEvent : DomainEvent
{
    public Order Order { get; init; }

    public OrderCreatedDomainEvent(Order order)
    {
        Order = order;
    }
}