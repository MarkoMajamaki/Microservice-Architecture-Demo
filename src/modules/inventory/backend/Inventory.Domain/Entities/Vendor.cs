using Shared.Domain;

namespace Inventory.Domain;

public class Vendor : Entity
{
    public List<Product> Products { get; set; }
}