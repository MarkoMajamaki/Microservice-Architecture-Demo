namespace Inventory.Api;

public class ProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string ImageId { get; set; }
}