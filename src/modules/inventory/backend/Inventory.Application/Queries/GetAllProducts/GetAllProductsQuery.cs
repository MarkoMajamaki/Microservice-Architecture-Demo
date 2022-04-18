using MediatR;

namespace Inventory.Application;

public class GetAllProductsQuery : IRequest<GetAllProductsQueryResponse>
{
}