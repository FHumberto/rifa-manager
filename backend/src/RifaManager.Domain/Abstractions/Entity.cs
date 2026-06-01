namespace RifaManager.Domain.Abstractions;

public abstract class Entity
{
    public Guid Id { get; private set; }

    protected Entity() => Id = Guid.NewGuid();

    public abstract void IsValid();
}
