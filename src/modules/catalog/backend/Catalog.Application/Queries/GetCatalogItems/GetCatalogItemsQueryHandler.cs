using MediatR;

namespace Catalog.Application;

public class GetCatalogItemsQueryHandler : IRequestHandler<GetCatalogItemsQuery, GetCatalogItemsQueryResponse>
{
    public Task<GetCatalogItemsQueryResponse> Handle(GetCatalogItemsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}