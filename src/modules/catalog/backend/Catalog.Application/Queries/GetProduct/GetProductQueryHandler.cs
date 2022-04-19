using MediatR;

namespace Catalog.Application;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductQueryResponse>
{
    public Task<GetProductQueryResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}