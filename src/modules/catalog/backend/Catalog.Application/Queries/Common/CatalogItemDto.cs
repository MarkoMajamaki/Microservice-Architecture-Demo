namespace Catalog.Application;

public class CategoryItemDto {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    List<CatalogItemDto> CatalogItems { get; set; }
}
