using Shared.Domain;

namespace Catalog.Domain;

public class Vendor : Entity
{
    public List<Product> Products { get; set; }
}