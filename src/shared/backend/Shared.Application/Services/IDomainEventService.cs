using MediatR;
using Shared.Domain;

namespace Shared.Application;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}