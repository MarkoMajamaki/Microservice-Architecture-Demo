using MediatR;
using Microsoft.Extensions.Logging;
using Order.Domain;
using Shared.Application;

namespace Order.Application;

/// <summary>
/// Domain event to create customer when order is created
/// </summary>
public class CreateCustomerEventHandler : INotificationHandler<DomainEventNotification<OrderCreatedEvent>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly  ILogger<CreateCustomerEventHandler> _logger;
    
    public CreateCustomerEventHandler(
        ILogger<CreateCustomerEventHandler> logger,
        ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<OrderCreatedEvent> notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create customer when order is created");

        return Task.CompletedTask;
    }
}