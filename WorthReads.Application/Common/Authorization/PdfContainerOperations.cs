using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Application.Common.Authorization;

public static class PdfContainerOperations
{
    public static OperationAuthorizationRequirement AddPdf = new() { Name = Constants.AddPdfOperationName };
}

public static class Constants
{
    public static readonly string AddPdfOperationName = "AddPdfToContainer";
}
