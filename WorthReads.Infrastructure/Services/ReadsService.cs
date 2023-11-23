using Domain.Reads;

namespace Infrastructure.Services;

public class ReadsService
{
    private readonly HttpClient _httpClient;

    public ReadsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Reads>> GetReads(string category, string type)
    {
        DateOnly date = DateOnly.FromDateTime(DateTime.Now);
        date = date.AddDays(-5);
        string dateString = date.ToString("yyyy-MM-dd");
        ArticleResult articleResult;
        var articleResult1 = await _httpClient.GetAsync($"/v2/everything?q={category}&language=en&sortBy={type}&pageSize=10&fromDate={dateString}&apiKey=ba8aff31801a46a6afbe9d6bb0215df9");

        if (articleResult1 is null)
        {
            articleResult = ArticleResult.EmptyArticleResult;
        }
        articleResult = ArticleResult.EmptyArticleResult;
        await Console.Out.WriteLineAsync($"{articleResult.Reads.First().Content.Length}");

        return articleResult.Reads;
    }
}
