using Shared.Domain;

namespace Order.Domain;

public class Product : Entity, IAggregateRoot
{
    public double Price { get; private set; }
}