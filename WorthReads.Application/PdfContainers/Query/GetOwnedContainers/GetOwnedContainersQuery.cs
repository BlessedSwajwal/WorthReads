using Application.PdfContainers.Common;
using MediatR;
using OneOf;
using WorthReads.Application.Common.Exceptions;
using WorthReads.Application.Common.Exceptions.ValidationException;

namespace Application.PdfContainers.Query.GetOwnedContainers;

public record GetOwnedContainersQuery(Guid UserId) : IRequest<OneOf<List<PdfContainerResult>, IServiceError, ValidationErrors>>;
