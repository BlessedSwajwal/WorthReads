using Domain.Reads.ValueObjects;
using WorthReads.Domain.Common.Models;

namespace Domain.Reads;

public class Reads : Entity<ReadsId>
{
    public Reads(ReadsId id, string source, string author, string title, string description, string url, string urlToImage, DateTime publishedAt, string content) : base(id)
    {
        Source = source;
        Author = author;
        Title = title;
        Description = description;
        Url = url;
        UrlToImage = urlToImage;
        PublishedAt = publishedAt;
        Content = content;
    }

    public string Source { get; private set; }
    public string Author { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Url { get; private set; }
    public string UrlToImage { get; private set; }
    public DateTime PublishedAt { get; private set; }
    public string Content { get; private set; }

    private Reads() { }
}
