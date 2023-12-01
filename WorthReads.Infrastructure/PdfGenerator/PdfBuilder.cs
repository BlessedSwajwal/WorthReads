using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Infrastructure.PdfGenerator;

public class PdfBuilder : IDocument
{
    public List<PdfArticle> Articles { get; }
    public static TextStyle paragraphStyle = TextStyle.Default.FontFamily(Fonts.Georgia).FontSize(14).FontColor("#333333").LineHeight((float)1.2);

    public PdfBuilder(List<PdfArticle> articles)
    {
        Articles = articles;
    }

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(20);

            page.Content().Element(ComposeContent);
        });
    }

    public void ComposeContent(IContainer container)
    {
        container.Column(col =>
        {
            foreach (var article in Articles)
            {
                if (article == PdfArticle.Empty) continue;

                col.Item().Text(article.Title).FontFamily(Fonts.TimesNewRoman).FontSize(18).FontColor("#000080")
                .BackgroundColor("#FFFDF0");

                if (article.Image != null)
                    col.Item().Image(article.Image);


                col.Item().Padding((float)2.0).Text(text =>
                {
                    text.ParagraphSpacing((float)2);
                    text.Line(article.Content);
                    text.DefaultTextStyle(paragraphStyle);
                });
                col.Item().PageBreak();
            }

        });
    }
}
