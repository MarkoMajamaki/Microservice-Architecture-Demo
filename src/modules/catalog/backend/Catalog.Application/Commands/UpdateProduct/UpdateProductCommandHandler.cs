using Catalog.Domain;
using MassTransit;
using MediatR;

namespace Catalog.Application;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>
{
    private IProductRepository _productRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public UpdateProductCommandHandler(
        IProductRepository productRepository,
        IPublishEndpoint publishEndpoint)
    {
        _productRepository = productRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await _productRepository.FindAsync(request.Id, cancellationToken);
        product.Name = request.Name;
        product.Description = request.Description;
        product.Quantity = request.Quantity;
        product.Price = request.Price;

        await _productRepository.SaveAsync(product, cancellationToken);

        // Publish integration event when product is updated
        await _publishEndpoint.Publish<ProductUpdatedIntegrationEvent>(
            new ProductUpdatedIntegrationEvent {
                Name = request.Name,
                Description = request.Description,
                Quantity = request.Quantity,
                Price = request.Price,
            }
        );

        return new UpdateProductCommandResponse();
    }
}