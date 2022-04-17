namespace Order.Domain;

public interface IOrderRepository
{
    Task<Order> GetOrderAsync(int orderId, CancellationToken cancellationToken);
    Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken cancellationToken);
    Task SaveAsync(Order order, CancellationToken cancellationToken);
}