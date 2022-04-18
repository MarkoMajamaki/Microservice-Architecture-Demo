using Inventory.Domain;
using Inventory.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure;

public class ProductRepository : IProductRepository
{
    private readonly InventoryContext _inventoryContext;
    
    public ProductRepository(InventoryContext inventoryContext)
    {
        _inventoryContext = inventoryContext;
    }
    
    public async Task<Product> FindAsync(int id, CancellationToken cancellationToken)
    {
        return await _inventoryContext.Products.FindAsync(id);
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _inventoryContext.Products.ToListAsync();
    }

    public async Task SaveAsync(Product product, CancellationToken cancellationToken)
    {
        _inventoryContext.Products.Add(product);

        // product.AddDomainEvent(ProductAddedEvent())

        await _inventoryContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        var productToUpdate = _inventoryContext.Products.Find(product.Id);
    
        productToUpdate.Quantity = product.Quantity;
        productToUpdate.Name = product.Name;
        productToUpdate.Description = product.Description;
        productToUpdate.Price = product.Price;
        
        // product.AddDomainEvent(ProductAddedEvent())

        await _inventoryContext.SaveChangesAsync();
    }
}