using Shared.Domain;

namespace Catalog.Domain;

public class Vendor : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Product> Products { get; set; }
}