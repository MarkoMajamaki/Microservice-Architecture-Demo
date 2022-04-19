using MediatR;

namespace Catalog.Application;

public class GetProductQuery : IRequest<GetProductQueryResponse>
{
    public int ProductId { get; init; }

    public GetProductQuery(int productId)
    {
        ProductId = productId;
    }
}