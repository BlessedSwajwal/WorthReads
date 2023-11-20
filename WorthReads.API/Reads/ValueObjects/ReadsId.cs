using WorthReads.Domain.Common.Models;

namespace Domain.Reads.ValueObjects;

public sealed class ReadsId : ValueObject
{
    public Guid Value { get; }
    private ReadsId(Guid v) { Value = v; }

    public static ReadsId Create(Guid value) => new(value);
    public static ReadsId CreateUnique() => new(Guid.NewGuid());
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
