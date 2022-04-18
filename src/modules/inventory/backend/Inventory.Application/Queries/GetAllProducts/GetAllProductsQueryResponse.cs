using Inventory.Domain;

namespace Inventory.Application;

public class GetAllProductsQueryResponse
{
    public IEnumerable<Product> Products { get; init; }
}