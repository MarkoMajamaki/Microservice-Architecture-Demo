using Shared.Domain;

namespace Order.Domain;

public class Status : ValueObject
{
    public int Code { get; private set; }

    public Status(int status)
    {
        Code = status;
    }
    public static Status Cancelled => new(0);
    public static Status Received => new(1);
    public static Status Handled => new(2);
    public static Status Completed => new(3);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }
}    
