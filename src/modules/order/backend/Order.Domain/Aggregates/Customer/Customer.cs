using Shared.Domain;

namespace Order.Domain;

public class Customer : Entity, IAggregateRoot
{    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Address Address { get; private set; }
}