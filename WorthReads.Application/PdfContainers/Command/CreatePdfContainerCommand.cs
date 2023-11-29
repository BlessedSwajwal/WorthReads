using Application.PdfContainers.Common;
using MediatR;
using OneOf;
using WorthReads.Application.Common.Exceptions;
using WorthReads.Application.Common.Exceptions.ValidationException;

namespace Application.PdfContainers.Command;

public record CreatePdfContainerCommand(Guid UserId, string Name) : IRequest<OneOf<PdfContainerResult, IServiceError, ValidationErrors>>;
