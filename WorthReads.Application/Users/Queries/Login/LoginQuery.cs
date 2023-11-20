using MediatR;
using OneOf;
using WorthReads.Application.Common.Exceptions;
using WorthReads.Application.Common.Exceptions.ValidationException;
using WorthReads.Application.Users.Common;

namespace WorthReads.Application.Users.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<OneOf<UserResponse, IServiceError, ValidationErrors>>;
