namespace Application.PdfContainers.Query.GetContainerPDF;

public record PDFResponse(byte[] PdfByteArray, string ContainerName);
