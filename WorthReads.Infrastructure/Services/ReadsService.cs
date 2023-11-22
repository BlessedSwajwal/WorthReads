using Domain.Reads;
using System.Net.Http.Json;

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
        ArticleResult? articleResult = await _httpClient.GetFromJsonAsync<ArticleResult>($"/v2/everything?q={category}&language=en&sortBy={type}&fromDate={dateString}&apiKey=ba8aff31801a46a6afbe9d6bb0215df9");

        if (articleResult is null) { articleResult = ArticleResult.EmptyArticleResult; }

        await Console.Out.WriteLineAsync($"{articleResult.Reads.First().Content.Length}");

        return articleResult.Reads;
    }
}
