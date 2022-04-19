using Shared.Domain;

namespace Catalog.Domain;

public class Category : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
}