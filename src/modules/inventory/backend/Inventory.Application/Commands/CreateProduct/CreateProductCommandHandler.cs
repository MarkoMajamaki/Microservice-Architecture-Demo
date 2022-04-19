using Inventory.Domain;
using MassTransit;
using MediatR;

namespace Inventory.Application;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
{
    private IProductRepository _productRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateProductCommandHandler(
        IProductRepository productRepository, 
        IPublishEndpoint publishEndpoint)
    {
        _productRepository = productRepository;
        _publishEndpoint = publishEndpoint;
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

        // Publish integration event when product is created
        await _publishEndpoint.Publish<ProductCreatedIntegrationEvent>(
            new ProductCreatedIntegrationEvent {
                Name = request.Name,
                Description = request.Description,
                Quantity = request.Quantity,
                Price = request.Price,
            }
        );

        return new CreateProductCommandResponse();
    }
}