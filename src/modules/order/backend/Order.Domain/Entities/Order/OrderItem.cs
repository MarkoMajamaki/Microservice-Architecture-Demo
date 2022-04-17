using Shared.Domain;

namespace Order.Domain;

public class OrderItem : Entity
{
    public int Amount { get; private set; }
    public int ProductId { get; private set; }
    // public Product Product { get; private set; }

    public OrderItem(int amount, int productId)
    {
        Amount = amount;
        ProductId = productId;
    }
}