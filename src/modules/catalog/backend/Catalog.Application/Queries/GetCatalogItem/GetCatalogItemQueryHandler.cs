using MediatR;

namespace Catalog.Application;

public class GetCatalogItemQueryHandler : IRequestHandler<GetCatalogItemQuery, GetCatalogItemQueryResponse>
{
    public Task<GetCatalogItemQueryResponse> Handle(GetCatalogItemQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}