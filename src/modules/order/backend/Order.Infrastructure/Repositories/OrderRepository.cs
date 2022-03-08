using Microsoft.EntityFrameworkCore;
using Order.Domain;

namespace Order.Infrastructure;
public class OrderRepository : IOrderRepository
{
    private readonly OrderContext _context;

    public OrderRepository(OrderContext context)
    {
        _context = context;
    }

    public async Task<Order.Domain.Order> GetOrderAsync(int orderId, CancellationToken cancellationToken)
    {
        return await _context.Orders.FindAsync(orderId);
    }

    public async Task<IEnumerable<Order.Domain.Order>> GetOrdersAsync(CancellationToken cancellationToken)
    {
        return await _context.Orders.ToListAsync(cancellationToken);
    }

    public async Task SaveAsync(Order.Domain.Order order, CancellationToken cancellationToken)
    {
        _context.Add(order);
        await _context.SaveChangesAsync(cancellationToken);
    }
}