using Shared.Domain;

namespace Order.Domain;

public class OrderPaidDomainEvent : DomainEvent
{   
    public Order Order { get; init; }

    public OrderPaidDomainEvent(Order order) 
    {
        Order = order;
    }
}