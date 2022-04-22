using Shared.Domain;

namespace Order.Domain;

public class OrderItem : Entity
{
    public int Quantity { get; private set; }
    public int ProductId { get; private set; }

    public OrderItem(int quantity, int productId)
    {
        Quantity = quantity;
        ProductId = productId;
    }
}