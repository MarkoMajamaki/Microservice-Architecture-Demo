using AutoMapper;
using MediatR;
using Order.Domain;

namespace Order.Application;

public static class GetOrderById
{
    public record Query(int id) : IRequest<Response>;

    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public Handler(
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderAsync(request.id, cancellationToken);
            return _mapper.Map<Response>(order);
        }
    }

    public class Response
    {
        public int OrderId { get; init; }
        public int CustomerId { get; init; }
        public List<OrderItem> Items { get; init; }
        public Address ShipToAddress { get; init; }

        public record OrderItem
        {
            public int ProductId { get; init; }
            public string ProductName { get; init; }
            public decimal Price { get; init; }
            public string PictureUrl { get; init; }
            public int Quantity { get; init; }
        }

        public record Address
        {
            public string Country { get; init; }
            public string City { get; init; }
            public string ZipCode { get; init; }  
            public string Street { get; init; }
        }
    }
}