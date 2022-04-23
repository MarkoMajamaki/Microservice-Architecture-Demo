using Shared.Domain;

namespace Order.Domain;

public class OrderCancelledDomainEvent : DomainEvent
{
    public Order Order { get; init; }

    public OrderCancelledDomainEvent(Order order) 
    {
        Order = order;
    }
}