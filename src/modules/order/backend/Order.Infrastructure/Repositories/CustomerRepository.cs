using Order.Domain;

namespace Order.Infrastructure;

public class CustomerRepository : ICustomerRepository
{
    private readonly OrderContext _context;

    public CustomerRepository(OrderContext context)
    {
        _context = context;
    }

    public async Task SaveAsync(Customer customer)
    {
        _context.Customer.Add(customer);
        await _context.SaveChangesAsync();
    }
}