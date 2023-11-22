using Domain.Reads;

namespace Infrastructure.Services;

internal class ArticleResult
{
    public string? Status;
    public int TotalResults;
    public List<Reads> Reads = new List<Reads>();

    public static readonly ArticleResult EmptyArticleResult = new ArticleResult();
}
