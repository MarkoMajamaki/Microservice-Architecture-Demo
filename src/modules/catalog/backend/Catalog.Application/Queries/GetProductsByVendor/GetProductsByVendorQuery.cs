using MediatR;

namespace Catalog.Application;

public class GetProductsByVendorQuery : IRequest<GetProductsByVendorQueryResponse>
{
    public int VendorId { get; init; }

    public GetProductsByVendorQuery(int vendorId)
    {
        VendorId = vendorId;
    }
}