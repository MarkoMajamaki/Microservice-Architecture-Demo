using FluentValidation;
using MediatR;
using Order.Domain;

namespace Order.Application;

public static class CreateOrder
{
    /// <summary>
    /// Create order command
    /// </summary>
    public class Command : IRequest<Response>
    {
        public int CustomerId { get; init; }
        public IEnumerable<OrderItem> Items { get; init; }
    }

    /// <summary>
    /// Create order command handler
    /// </summary>
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
                order.AddItem(new Domain.OrderItem(orderItem.Quantity, orderItem.Id));
            }

            order.AddDomainEvent(new OrderCreatedEvent(order));

            await _orderRepository.SaveAsync(order, cancellationToken);

            return new Response(order.Id, Status.Received);        
        }
    }

    /// <summary>
    /// Create order command response
    /// </summary>
    public record Response(int Id, Status status);

    /// <summary>
    /// Create order command validator
    /// </summary>
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.CustomerId).NotNull().NotEmpty();
            RuleFor(x => x.Items).NotEmpty();
        }
    }
}