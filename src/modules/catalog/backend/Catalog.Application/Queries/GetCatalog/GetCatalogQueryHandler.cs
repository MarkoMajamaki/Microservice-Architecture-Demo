using MediatR;

namespace Catalog.Application;

public class GetCatalogQueryHandler : IRequestHandler<GetCatalogQuery, GetCatalogQueryResponse>
{
    public Task<GetCatalogQueryResponse> Handle(GetCatalogQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}