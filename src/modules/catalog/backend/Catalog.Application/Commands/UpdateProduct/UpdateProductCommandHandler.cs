using Catalog.Domain;
using MediatR;

namespace Catalog.Application;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>
{
    private IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await _productRepository.FindAsync(request.Id, cancellationToken);
        product.Name = request.Name;
        product.Description = request.Description;
        product.Quantity = request.Quantity;
        product.Price = request.Price;

        await _productRepository.SaveAsync(product, cancellationToken);

        return new UpdateProductCommandResponse();
    }
}