namespace Contracts;

public record AddPdfToContainerRequest(Guid ContainerId, Uri Url);
