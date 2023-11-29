using WorthReads.Domain.Common.Models;
using WorthReads.Domain.Users.ValueObjects;

namespace Domain.PdfContainer;

public class PdfContainer : Entity<PdfContainerId>
{
    public string Name { get; private set; }
    public bool IsPublic { get; private set; } = false;
    public UserId OwnerId { get; private set; }
    private List<Uri> _readsUrl = new();
    public IReadOnlyList<Uri> ReadsUrl => _readsUrl.AsReadOnly();

    public readonly static PdfContainer ContainerEmpty = new(PdfContainerId.Create(Guid.Empty), UserId.Create(Guid.Empty), string.Empty);

    private PdfContainer(PdfContainerId id, UserId ownerId, string name) : base(id)
    {
        OwnerId = ownerId;
        Name = name;
    }

    public static PdfContainer Create(UserId OwnerId, string name)
    {
        return new(PdfContainerId.CreateUnique(), OwnerId, name);
    }

    public void AddReadsUrl(Uri readUrl)
    {
        _readsUrl.Add(readUrl);
    }

    public void RemoveReads(Uri Url)
    {
        _readsUrl.Remove(Url);
    }

    public void ChangeName(string name)
    {
        Name = name;
    }

    public void MakePublic()
    {
        IsPublic = true;
    }

    public void MakePrivate()
    {
        IsPublic = false;
    }
}
