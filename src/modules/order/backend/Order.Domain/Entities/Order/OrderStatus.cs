using Shared.Domain;

namespace Order.Domain;

public class OrderStatus : ValueObject
{
    public int Code { get; private set; }
    public string Description { get; private set; }

    private OrderStatus()
    {
        // required by EF
    }

    public OrderStatus(int status, string description)
    {
        Code = status;
        Description = description;
    }
    public static OrderStatus Created => new(1, nameof(Created).ToLowerInvariant());
    public static OrderStatus StockConfirmed => new(2, nameof(StockConfirmed).ToLowerInvariant());
    public static OrderStatus Paid => new(3, nameof(Paid).ToLowerInvariant());
    public static OrderStatus Shipped => new(4, nameof(Shipped).ToLowerInvariant());
    public static OrderStatus Cancelled => new(5, nameof(Cancelled).ToLowerInvariant());

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }
}    
