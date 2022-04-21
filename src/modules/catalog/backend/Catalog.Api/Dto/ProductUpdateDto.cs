namespace Catalog.Api;

public class ProductUpdateDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Quantity { get; init; }
    public double Price { get; init; }
}