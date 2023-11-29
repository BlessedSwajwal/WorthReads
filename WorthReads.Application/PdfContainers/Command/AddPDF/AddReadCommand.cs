using MediatR;
using OneOf;
using OneOf.Types;
using System.Security.Claims;
using WorthReads.Application.Common.Exceptions;
using WorthReads.Application.Common.Exceptions.ValidationException;

namespace Application.PdfContainers.Command.AddPDF;

public record AddReadCommand(ClaimsPrincipal User, Guid ContainerId, Uri url) : IRequest<OneOf<Some, IServiceError, ValidationErrors>>;
