using WorthReads.Domain.Common.Models;
using WorthReads.Domain.Users;

namespace Domain.PdfContainer;

public class PdfContainer : Entity<PdfContainerId>
{
    public bool IsPublic { get; private set; } = false;
    public User Owner { get; private set; }
    private List<string> _readsUrl = new();
    public IReadOnlyList<string> ReadsUrl => _readsUrl.AsReadOnly();

    private PdfContainer(PdfContainerId id, User owner) : base(id)
    {
        Owner = owner;
    }

    public static PdfContainer Create(User Owner)
    {
        return new(PdfContainerId.CreateUnique(), Owner);
    }

    public void AddReadsUrl(string readUrl)
    {
        _readsUrl.Add(readUrl);
    }

    public void RemoveReads(string Url)
    {
        _readsUrl.Remove(Url);
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
