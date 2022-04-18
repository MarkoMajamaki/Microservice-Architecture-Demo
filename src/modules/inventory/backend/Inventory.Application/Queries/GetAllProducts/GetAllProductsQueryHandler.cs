using MediatR;

namespace Inventory.Application;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, GetAllProductsQueryResponse>
{
    private IProductRepository _productRepository;

    public GetAllProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<GetAllProductsQueryResponse> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return new GetAllProductsQueryResponse()
        {
            Products = await _productRepository.GetAllAsync(cancellationToken)
        };
    }
}