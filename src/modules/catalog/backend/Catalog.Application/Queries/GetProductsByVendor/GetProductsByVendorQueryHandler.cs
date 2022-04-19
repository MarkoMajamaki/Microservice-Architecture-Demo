using MediatR;

namespace Catalog.Application;

public class GetProductsVendorQueryHandler : IRequestHandler<GetProductsByVendorQuery, GetProductsByVendorQueryResponse>
{
    public Task<GetProductsByVendorQueryResponse> Handle(GetProductsByVendorQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}