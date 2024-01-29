using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace Infrastructure.PdfGenerator;

internal class HtmlConverterToPdf
{
    public static byte[] Generate(List<PdfArticle> PdfArticles)
    {
        using PdfSharp.Pdf.PdfDocument finalPdfDocument = new PdfSharp.Pdf.PdfDocument();
        foreach (var PdfArticle in PdfArticles)
        {

            //Adding some style for generating pdf like fontsize and image height, width
            var sb = new StringBuilder();
            sb.Append("""
                        <html>
            <head>
            <style>
            * {
                font-size: 22;
            }

            img {
            max-height: 300px;
            width: 100%;
            }
            </style>
            </head>
            <body>
            """);

            sb.Append(PdfArticle.Content);
            sb.Append("""</body> </html>""");

            //Creating pdf from html and saving it to memory stream.

            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(sb.ToString());

            //Merging pdfs one by one.
            using var stream = new MemoryStream();
            doc.Save(stream);
            using var tempDoc = PdfReader.Open(stream, PdfDocumentOpenMode.Import);
            foreach (var page in tempDoc.Pages)
            {
                finalPdfDocument.AddPage(page);
            }
        }
        using var finalPdfStream = new MemoryStream();
        finalPdfDocument.Save(finalPdfStream);
        byte[] pdfResult = finalPdfStream.ToArray();
        return pdfResult;
    }
}
