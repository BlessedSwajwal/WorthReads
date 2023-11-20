using MediatR;
using OneOf;
using WorthReads.Application.Common.Exceptions;
using WorthReads.Application.Common.Exceptions.ValidationException;

namespace Application.Reads.Query;

public record GetReadsQuery(string Status = "Default") : IRequest<OneOf<List<Domain.Reads.Reads>, IServiceError, ValidationErrors>>;
