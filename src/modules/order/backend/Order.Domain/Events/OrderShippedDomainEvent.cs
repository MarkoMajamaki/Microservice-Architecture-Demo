using Shared.Domain;

namespace Order.Domain;

public class OrderShippedDomainEvent : DomainEvent
{
    public Order Order { get; init; }

    public OrderShippedDomainEvent(Order order) 
    {
        Order = order;
    }
}