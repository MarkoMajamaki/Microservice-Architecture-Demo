namespace Order.Api;

public class CreateOrderRequest
{
    public int CustomerId { get; set; }
    public List<OrderItem> OrderItems { get; set; }

    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}
