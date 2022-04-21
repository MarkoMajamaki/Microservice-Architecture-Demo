using MediatR;
using Order.Domain;

namespace Order.Application;

public static class CreateOrder
{
    public record Command(int CustomerId, IEnumerable<OrderItem> Items) : IRequest<Response>;

    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly IOrderRepository _orderRepository;

        public Handler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var order = new Order.Domain.Order(request.CustomerId);

            foreach (OrderItem orderItem in request.Items)
            {
                order.AddItem(new Domain.OrderItem(orderItem.Amount, orderItem.Id));
            }

            order.AddDomainEvent(new OrderCreatedEvent(order));

            await _orderRepository.SaveAsync(order, cancellationToken);

            return new Response(order.Id, Status.Received);        
        }
    }

    public record Response(int Id, Status status);
}