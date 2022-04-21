using Catalog.Domain;
using MassTransit;
using MediatR;
using Shared.Application.IntegrationEvents;

namespace Catalog.Application;

public static class UpdateProduct
{
    public record Command : IRequest<Response>
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public int Quantity { get; init; }
        public double Price { get; init; }
    }

    public class Handler : IRequestHandler<Command, Response>
    {        
        private IProductRepository _productRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public Handler(
            IProductRepository productRepository,
            IPublishEndpoint publishEndpoint)
        {
            _productRepository = productRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
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

            return new Response();
        }        
    }

    public record Response();
}