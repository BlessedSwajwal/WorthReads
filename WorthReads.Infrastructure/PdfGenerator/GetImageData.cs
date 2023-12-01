namespace Infrastructure.PdfGenerator;

public class GetImageData
{
    private readonly HttpClient _httpClient;

    public GetImageData(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<PdfArticle>> Get(IEnumerable<PdfArticle> articles)
    {
        var updatedArticles = new List<PdfArticle>();
        foreach (PdfArticle article in articles)
        {
            if (article == PdfArticle.Empty) continue;
            var response = await _httpClient.GetAsync(article.Lead_Image_Url);
            response.EnsureSuccessStatusCode();
            article.Image = await response.Content.ReadAsByteArrayAsync();
            updatedArticles.Add(article);
        }
        return updatedArticles;
    }
}
