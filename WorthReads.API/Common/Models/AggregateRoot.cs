namespace WorthReads.Domain.Common.Models;

public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : notnull
{
    public new TId Id { get; protected set; }

    protected AggregateRoot(TId id)
    {
        Id = id;
    }

    protected AggregateRoot()
    {
    }
}
