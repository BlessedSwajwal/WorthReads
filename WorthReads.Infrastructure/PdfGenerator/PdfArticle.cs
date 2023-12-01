namespace Infrastructure.PdfGenerator;

public class PdfArticle
{
    public static PdfArticle Empty = new PdfArticle();
    public string? Title { get; set; }
    public Uri? Url { get; set; }
    public Uri? Lead_Image_Url { get; set; }
    public byte[]? Image { get; set; }
    public string? Content { get; set; }

}
