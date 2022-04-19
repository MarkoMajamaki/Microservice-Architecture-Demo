using MediatR;

namespace Catalog.Application;

public class GetCatalogItemsCategoryQueryHandler : IRequestHandler<GetCatalogCategoryQuery, GetCatalogCategoryQueryResponse>
{
    public Task<GetCatalogCategoryQueryResponse> Handle(GetCatalogCategoryQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}