using System.Collections;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Domain;

namespace Order.Application;

public static class GetAllOrders
{
    public record Query : IRequest<Response>;

    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly ILogger<Handler> _logger;
        private readonly IOrderRepository _orderRepository;

        public Handler(
            ILogger<Handler> logger,
            IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersAsync(cancellationToken);
            return new Response(orders);
        }
    }

    public record Response(IEnumerable<Domain.Order> Orders);
}