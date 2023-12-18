using WorthReads.Domain.Common.Models;

namespace Domain.PdfContainer;

public class PdfContainerId : ValueObject
{
    public Guid Value { get; private set; }

    private PdfContainerId(Guid val)
    {
        Value = val;
    }

    public static PdfContainerId Create(Guid val) => new PdfContainerId(val);
    public static PdfContainerId CreateUnique() => new PdfContainerId(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private PdfContainerId() { }
}
