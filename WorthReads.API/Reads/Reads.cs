using Domain.Reads.ValueObjects;
using WorthReads.Domain.Common.Models;

namespace Domain.Reads;

public class Reads : Entity<ReadsId>
{
    private Reads(ReadsId id, string source, string title, string description, string url, string urlToImage) : base(id)
    {
        Source = source;
        Title = title;
        Description = description;
        Url = url;
        UrlToImage = urlToImage;
    }

    public static Reads Create(string source, string title, string description, string url, string urlToImage)
    {
        return new(ReadsId.CreateUnique(), source, title, description, url, urlToImage);
    }

    public static readonly Reads EmptyReads = new();

    public string Source { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Url { get; private set; }
    public string UrlToImage { get; private set; }

    private Reads() { }
}
