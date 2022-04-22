using Shared.Domain;

namespace Order.Domain;

public class Order : Entity, IAggregateRoot
{
    public Status Status { get; private set; }
    public int CustomerId { get; private set; }
    public IList<OrderItem> Items { get; private set; }

    public Order(int customerId)
    {
        CustomerId = customerId;
        Items = new List<OrderItem>();

    }

    public void AddItem(OrderItem item)
    {
        Items.Add(item);
    }

    public void ChangeStatus(Status status)
    {

    }
}