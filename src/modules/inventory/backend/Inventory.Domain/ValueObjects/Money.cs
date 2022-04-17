using Shared.Domain;

public class Money : ValueObject
{
    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}