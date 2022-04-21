using MediatR;
using Order.Domain;

namespace Order.Application;

public static class GetOrderById
{
    public record Query(int id) : IRequest<Response>;

    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IOrderRepository _orderRepository;

        public Handler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _orderRepository.GetOrderAsync(request.id, cancellationToken);

            return new Response {
                Id = result.Id, 
                CustomerId = result.CustomerId
            };
        }
    }

    public class Response
    {
        public int Id { get; init; }
        public int CustomerId { get; init; }
    }
}