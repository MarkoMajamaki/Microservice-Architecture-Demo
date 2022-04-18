using Inventory.Domain;

public interface IProductRepository
{
    Task SaveAsync(Product product, CancellationToken cancellationToken);
    Task<Product> FindAsync(int id, CancellationToken cancellationToken);
    Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);
    Task UpdateAsync(Product product, CancellationToken cancellationToken);
 }  
