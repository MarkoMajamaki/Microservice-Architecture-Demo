using MediatR;
using Microsoft.Extensions.Logging;
using Order.Domain;

namespace Order.Application;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, GetAllOrdersResponse>
{
    private readonly ILogger<GetAllOrdersQueryHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public GetAllOrdersQueryHandler(
        ILogger<GetAllOrdersQueryHandler> logger,
        IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task<GetAllOrdersResponse> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrdersAsync(cancellationToken);
        return new GetAllOrdersResponse(order);
    }
}