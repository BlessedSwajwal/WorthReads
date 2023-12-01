using MediatR;
using OneOf;
using System.Security.Claims;
using WorthReads.Application.Common.Exceptions;
using WorthReads.Application.Common.Exceptions.ValidationException;

namespace Application.PdfContainers.Query.GetContainerPDF;

public record GetContainerPDFQuery(ClaimsPrincipal User, Guid ContainerId) : IRequest<OneOf<PDFResponse, IServiceError, ValidationErrors>>;
