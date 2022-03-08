using Shared.Domain;

namespace Order.Domain;

public class Product : Entity, IAggregateRoot
{
    public string Name { get; private set; }  
    public string Description { get; private set; }
    public double Price { get; private set; }
}