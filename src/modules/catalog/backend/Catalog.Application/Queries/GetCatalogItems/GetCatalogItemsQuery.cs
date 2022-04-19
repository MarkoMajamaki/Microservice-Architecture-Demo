using MediatR;

namespace Catalog.Application;

public class GetCatalogItemsQuery : IRequest<GetCatalogItemsQueryResponse>
{
}