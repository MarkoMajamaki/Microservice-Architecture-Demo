namespace Shared.Domain;

public interface IHasDomainEvent
{
    public IReadOnlyCollection<DomainEvent> DomainEvents { get; }
}

public abstract class DomainEvent
{
    protected DomainEvent()
    {
        DateOccurred = DateTimeOffset.UtcNow;
    }

    public bool IsPublished { get; set; }
    
    public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
}