using MediatR;
using Order.Domain;

namespace Order.Application;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, GetOrderByIdResponse>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<GetOrderByIdResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _orderRepository.GetOrderAsync(request.id, cancellationToken);
        return new GetOrderByIdResponse() {
            Id = result.Id, 
            CustomerId = result.CustomerId
        };
    }
}