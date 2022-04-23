public class GetAllOrdersResponse
{
    public List<Order> Orders { get; init; }
    public class Order 
    {
        public int OrderId { get; init; }
        public int CustomerId { get; init; }
        public List<OrderItem> Items { get; init; }
        public Address ShipToAddress { get; set; }

        public record OrderItem
        {
            public int ProductId { get; init; }
            public string ProductName { get; init; }
            public decimal Price { get; init; }
            public string PictureUrl { get; init; }
            public int Quantity { get; init; }
        }

        public record Address
        {
            public string Country { get; init; }
            public string City { get; init; }
            public string ZipCode { get; init; }  
            public string Street { get; init; }
        }
    }
}