namespace Shared.Domain;

public abstract class Entity : IHasDomainEvent
{   
    private List<DomainEvent> _domainEvents;

    /// <summary>
    /// Entity Id
    /// </summary>
    public int Id { get; protected set; }

    /// <summary>
    /// Timestamp when entity is created. Added automatically in DbContext class.
    /// </summary>
    public DateTime? Created { get; protected set; }

    /// <summary>
    /// Who is created this entity. Added automatically in DbContext class.
    /// </summary>
    public Guid? CreatedBy { get; protected set; }

    /// <summary>
    /// Time when entity was previously modifed. Added automatically in DbContext class.
    /// </summary>
    public DateTime? LastModified { get; protected set; }

    /// <summary>
    /// Who modifed this entity previously. Added automatically in DbContext class.
    /// </summary>
    public Guid? LastModifiedBy { get; protected set; }

    /// <summary>
    /// Get domain events
    /// </summary>
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

    public Entity()
    {
        _domainEvents = new List<DomainEvent>();
    }

    public void SetCreated(DateTime? createdDateTime)
    {
        if (Created.HasValue)
        {
            throw new InvalidOperationException("Entity already has created timestamp");
        }

        Created = createdDateTime;
    }

    public void SetCreatedBy(Guid? createdBy)
    {
        if (CreatedBy.HasValue)
        {
            throw new InvalidOperationException("Entity already has CreatedBy value");
        }

        CreatedBy = createdBy;
    }

    public void UpdateLastModified(DateTime? lastModified)
    {
        LastModified = lastModified;
    }

    public void UpdateLastModifiedBy(Guid? lastModifiedBy)
    {
        LastModifiedBy = lastModifiedBy;
    }

    public void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents?.Remove(domainEvent);        
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}
