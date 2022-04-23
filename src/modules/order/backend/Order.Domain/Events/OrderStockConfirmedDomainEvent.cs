using Shared.Domain;

namespace Order.Domain;

public class OrderStockConfirmedDomainEvent : DomainEvent
{
    public Order Order { get; init; }

    public OrderStockConfirmedDomainEvent(Order order) 
    {
        Order = order;
    }
}