using Application.Common.Services;
using Domain.Reads;

namespace Infrastructure.PdfGenerator;

//Has to get the article content.
//Check if the pdfArticle is Emoty pdfArticle. otherwise null reference exception will be thrown.
//Has to get the image for each article content.
public class GenerateContainerPDF : IGenerateContainerPdf
{
    private readonly GetArticleContent _getArticleContent;

    public GenerateContainerPDF(GetArticleContent getArticleContent)
    {
        _getArticleContent = getArticleContent;
    }

    public async Task<byte[]> GenerateContainerPdf(List<Read> container)
    {
        //Get only the urls.
        var urls = container.Select(c => c.Url);

        var pdfArticles = await _getArticleContent.GetContent(urls);

        //pdfArticles.Content has html string. Generate pdf from it.

        var result = HtmlConverterToPdf.Generate(pdfArticles);
        return result;

    }
}
