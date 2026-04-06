namespace Kompass.Domain.Common.Entities;

public abstract class Entity<T>
{
    public virtual T Id { get; protected set; } = default!;
}