namespace SharedKernel;

public abstract class Entity
{
    private readonly List<DomainEvent> _domainEvents = new();

    protected Entity(int id)
    {
        Id = id;
    }

    protected Entity()
    {
    }

    public int Id { get; init; }
    
    public IReadOnlyCollection<DomainEvent> GetDomainEvents() => _domainEvents.ToList();
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
