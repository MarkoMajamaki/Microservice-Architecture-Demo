using Shared.Domain;

namespace Order.Domain;

public class OrderItem : Entity
{
    public int ProductId { get; private set; }
    public string ProductName { get; private set; }
    public decimal Price { get; private set; }
    public string PictureUrl { get; private set; }
    public int Quantity { get; private set; }

    private OrderItem()
    {
        // required by EF
    }

    public OrderItem(
        int productId,
        string productName,
        decimal price,
        string pictureUrl,
        int quantity)
    {
        ProductId = productId;
        ProductName = productName;
        Price = price;
        PictureUrl = pictureUrl;
        Quantity = quantity;
    }

    public void IncreaseQuantity(int quantity)
    {
        Quantity += quantity;
    }
}