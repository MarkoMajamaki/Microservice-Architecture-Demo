using Catalog.Domain;
using MediatR;

namespace Catalog.Application;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
{
    private IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = new Product();
        product.Name = request.Name;
        product.Description = request.Description;
        product.Quantity = request.Quantity;
        product.Price = request.Price;

        // Save product to database
        await _productRepository.SaveAsync(product, cancellationToken);

        return new CreateProductCommandResponse();
    }
}