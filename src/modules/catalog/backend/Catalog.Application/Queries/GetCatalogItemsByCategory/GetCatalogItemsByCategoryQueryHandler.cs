using MediatR;

namespace Catalog.Application;

public class GetCatalogItemsCategoryQueryHandler : IRequestHandler<GetCatalogItemsByCategoryQuery, GetCatalogItemsByCategoryQueryResponse>
{
    public Task<GetCatalogItemsByCategoryQueryResponse> Handle(GetCatalogItemsByCategoryQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}