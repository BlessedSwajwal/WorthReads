namespace Application.PdfContainers.Common;

public record PdfContainerResult(string Name, Guid OwnerId, List<string> ReadsUrls, bool IsPublic);

