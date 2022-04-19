using MediatR;

namespace Catalog.Application;

public class GetCatalogCategoryQuery : IRequest<GetCatalogCategoryQueryResponse>
{
    public int CategoryId { get; init; }

    public GetCatalogCategoryQuery(int categoryId)
    {
        CategoryId = categoryId;
    }
}