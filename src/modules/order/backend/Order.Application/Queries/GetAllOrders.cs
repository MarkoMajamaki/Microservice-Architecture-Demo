using System.Collections;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public Handler(
            ILogger<Handler> logger,
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersAsync(cancellationToken);
            return _mapper.Map<Response>(orders);
        }
    }

    public class Response
    {
        public List<Order> Orders { get; init; }
        public class Order 
        {
            public int OrderId { get; init; }
            public int CustomerId { get; init; }
            public List<OrderItem> Items { get; init; }
            public Address ShipToAddress { get; set; }

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
}