using Shared.Domain;

namespace Inventory.Domain;

public class Product : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string ImageId { get; set; }
}