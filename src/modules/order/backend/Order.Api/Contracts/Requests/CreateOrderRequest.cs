namespace Order.Api;

public class CreateOrderRequest
{
    public int CustomerId { get; init; }
    public Address ShipToAddress { get; init; }
    public List<OrderItem> Items { get; init; }

    public class OrderItem
    {
        public int ProductId { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public int Quantity { get; init; }
    }

    public class Address
    {
        public string Country { get; init; }
        public string City { get; init; }
        public string ZipCode { get; init; }  
        public string Street { get; init; }
    }
}
