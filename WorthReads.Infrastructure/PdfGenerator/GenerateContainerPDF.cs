using Application.Common.Services;
using Domain.Reads;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Infrastructure.PdfGenerator;

//Has to get the article content.
//Check if the pdfArticle is Emoty pdfArticle. otherwise null reference exception will be thrown.
//Has to get the image for each article content.
public class GenerateContainerPDF : IGenerateContainerPdf
{
    private readonly GetArticleContent _getArticleContent;
    private readonly GetImageData _getImageData;

    public GenerateContainerPDF(GetArticleContent getArticleContent, GetImageData getImageData)
    {
        _getArticleContent = getArticleContent;
        _getImageData = getImageData;
    }

    public async Task<byte[]> GenerateContainerPdf(List<Read> container)
    {
        var urls = container.Select(c => c.Url);
        var pdfArticles = await _getArticleContent.GetContent(urls);
        var articlesWithImage = await _getImageData.Get(pdfArticles);

        QuestPDF.Settings.License = LicenseType.Community;
        var doc = new PdfBuilder(articlesWithImage);
        var pdfByteArray = doc.GeneratePdf();
        return pdfByteArray;
    }
}
