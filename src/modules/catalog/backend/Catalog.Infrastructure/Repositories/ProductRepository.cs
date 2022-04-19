using Catalog.Domain;

namespace Catalog.Infrastructure;

public class ProductRepository : IProductRepository
{    
    public ProductRepository()
    {
    }
    
    public Task<Product> FindAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(Product product, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}