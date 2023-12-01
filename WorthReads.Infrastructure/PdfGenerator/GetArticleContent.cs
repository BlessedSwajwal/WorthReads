using HtmlAgilityPack;
using System.Net.Http.Json;
using System.Text;

namespace Infrastructure.PdfGenerator;

public class GetArticleContent
{
    private readonly HttpClient _httpClient;
    private readonly static Uri BaseUri = new Uri("http://127.0.0.1:32768");

    public GetArticleContent(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<PdfArticle>> GetContent(IEnumerable<Uri> urls)
    {
        List<PdfArticle> pdfArticles = new List<PdfArticle>();

        foreach (Uri url in urls)
        {
            var response = await _httpClient.GetFromJsonAsync<PdfArticle>(new Uri($"{BaseUri.OriginalString}/parser?url={url.OriginalString}"));

            if (response is null)
            {
                pdfArticles.Add(PdfArticle.Empty);
                continue;
            }
            response.Content = SanitizeContent(response.Content!);
            pdfArticles.Add(response);
        }

        return pdfArticles;
    }

    private static string SanitizeContent(string content)
    {
        StringBuilder sb = new StringBuilder();
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(content);
        var paragraphs = doc.DocumentNode.Descendants("p");
        foreach (var paragraph in paragraphs)
        {
            sb.AppendLine(HtmlEntity.DeEntitize(paragraph.InnerText));
        }
        return sb.ToString();
    }
}
