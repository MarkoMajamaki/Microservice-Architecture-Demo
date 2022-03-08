using MediatR;
using Order.Domain;

namespace Order.Application;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order.Domain.Order(request.CustomerId);

        foreach (OrderItem orderItem in request.Items)
        {
            order.AddItem(new Domain.OrderItem(orderItem.Amount, orderItem.Id));
        }

        await _orderRepository.SaveAsync(order, cancellationToken);

        return new CreateOrderResponse(order.Id, Status.Received);
    }
}