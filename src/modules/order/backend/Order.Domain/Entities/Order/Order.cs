using Shared.Domain;

namespace Order.Domain;

public class Order : Entity, IAggregateRoot
{
    public Address ShipToAddress { get; private set; }
    public OrderStatus Status { get; private set; }
    public int CustomerId { get; private set; }
    public IList<OrderItem> Items { get; private set; }

    private Order()
    {
        // required by EF
    }
    
    public Order(int customerId, Address shipToAddress)
    {
        CustomerId = customerId;
        ShipToAddress = shipToAddress;
        Items = new List<OrderItem>();
        Status = OrderStatus.Created;
    }

    public void AddItem(OrderItem item)
    {
        var existingOrderItem = Items.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
        if (existingOrderItem != null)
        {
            existingOrderItem.IncreaseQuantity(item.Quantity);
        }
        else 
        {
            Items.Add(item);
        }
    }

    public void ChangeStatus(OrderStatus newStatus)
    {
        if (newStatus == Status)
        {
            return;
        }
        
        DomainEvent domainEvent = null;

        if (newStatus == OrderStatus.StockConfirmed && Status == OrderStatus.Created)
        {
            domainEvent = new OrderStockConfirmedDomainEvent(this);
        }
        else if (newStatus == OrderStatus.Paid && Status == OrderStatus.StockConfirmed)
        {
            domainEvent = new OrderPaidDomainEvent(this);
        }
        else if (newStatus == OrderStatus.Shipped && Status == OrderStatus.Paid)
        {
            domainEvent = new OrderShippedDomainEvent(this);
        }
        else if (newStatus == OrderStatus.Cancelled && Status != OrderStatus.Paid)
        {
            domainEvent = new OrderCancelledDomainEvent(this);
        }
        else 
        {
            throw new Exception($"It is not possible change status from {Status.Description} to {newStatus.Description}");
        }

        // Remove previous added domain events which are not valid anymore
        RemoveDomainEventsByType(typeof(OrderStockConfirmedDomainEvent));
        RemoveDomainEventsByType(typeof(OrderPaidDomainEvent));
        RemoveDomainEventsByType(typeof(OrderShippedDomainEvent));
        RemoveDomainEventsByType(typeof(OrderCancelledDomainEvent));

        // Add new domain event
        AddDomainEvent(domainEvent);

        Status = newStatus;
    }
}