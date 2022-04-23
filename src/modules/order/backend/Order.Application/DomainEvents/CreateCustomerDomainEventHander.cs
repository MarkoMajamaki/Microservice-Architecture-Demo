using MediatR;
using Microsoft.Extensions.Logging;
using Order.Domain;
using Shared.Application;

namespace Order.Application;

/// <summary>
/// Domain event to create customer when order is created
/// </summary>
public class CreateCustomerDomainEventHandler : INotificationHandler<DomainEventNotification<OrderCreatedDomainEvent>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly  ILogger<CreateCustomerDomainEventHandler> _logger;
    
    public CreateCustomerDomainEventHandler(
        ILogger<CreateCustomerDomainEventHandler> logger,
        ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<OrderCreatedDomainEvent> notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create customer when order is created");

        return Task.CompletedTask;
    }
}