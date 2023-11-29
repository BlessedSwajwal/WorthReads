namespace Application.PdfContainers.Common;

public record PdfContainerResult(Guid id, string Name, Guid OwnerId, List<string> ReadsUrls, bool IsPublic);

