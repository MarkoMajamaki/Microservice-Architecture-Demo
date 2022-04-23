using AutoMapper;
using FluentValidation;
using MediatR;
using Order.Domain;

namespace Order.Application;

public static class CreateOrder
{
    /// <summary>
    /// Create order command
    /// </summary>
    public class Command : IRequest<Response>
    {
        public int CustomerId { get; init; }
        public Address ShipToAddress { get; set; }
        public List<OrderItem> Items { get; init; }

        public record OrderItem
        {
            public int ProductId { get; init; }
            public string Name { get; init; }
            public decimal Price { get; init; }
            public int Quantity { get; init; }
            public string PictureUrl { get; init; }
        }

        public record Address
        {
            public string Country { get; init; }
            public string City { get; init; }
            public string ZipCode { get; init; }  
            public string Street { get; init; }
        }
    }

    /// <summary>
    /// Create order command handler
    /// </summary>
    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly IOrderRepository _orderRepository;

        public Handler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var order = new Order.Domain.Order(
                request.CustomerId, 
                new Address(
                    request.ShipToAddress.Country, 
                    request.ShipToAddress.City, 
                    request.ShipToAddress.ZipCode, 
                    request.ShipToAddress.Street));

            foreach (var orderItem in request.Items)
            {
                var domainOrderItem = new OrderItem(
                    orderItem.ProductId,
                    orderItem.Name,
                    orderItem.Price,
                    orderItem.PictureUrl,
                    orderItem.Quantity
                );

                order.AddItem(domainOrderItem);
            }
            
            order.AddDomainEvent(new OrderCreatedDomainEvent(order));

            await _orderRepository.SaveAsync(order, cancellationToken);

            return new Response(order.Id, order.Status);        
        }
    }

    /// <summary>
    /// Create order command response
    /// </summary>
    public record Response(int Id, OrderStatus status);

    /// <summary>
    /// Create order command validator
    /// </summary>
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty();
                
            RuleFor(x => x.Items)
                .NotEmpty();

            RuleFor(x => x.ShipToAddress)
                .NotNull()
                .ChildRules(x => x.RuleFor(x => x.City)
                    .NotEmpty())
                .ChildRules(x => x.RuleFor(x => x.Country)
                    .NotEmpty())
                .ChildRules(x => x.RuleFor(x => x.Street)
                    .NotEmpty())
                .ChildRules(x => x.RuleFor(x => x.ZipCode)
                    .NotEmpty());
        }
    }
}