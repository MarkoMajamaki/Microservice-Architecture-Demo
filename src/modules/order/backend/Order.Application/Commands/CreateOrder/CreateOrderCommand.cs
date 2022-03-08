using MediatR;

namespace Order.Application;

public record CreateOrderCommand(int CustomerId, IEnumerable<OrderItem> Items) : IRequest<CreateOrderResponse>;

public record OrderItem(int Id, int Amount);