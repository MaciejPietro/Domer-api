namespace Domer.Domain.Common.Entities;

public abstract class Entity<T>
{
    public virtual T Id { get; set; } = default!;
}