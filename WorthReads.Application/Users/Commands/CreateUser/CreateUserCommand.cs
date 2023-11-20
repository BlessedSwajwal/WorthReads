using MediatR;
using OneOf;
using WorthReads.Application.Common.Exceptions;
using WorthReads.Application.Common.Exceptions.ValidationException;
using WorthReads.Application.Users.Common;

namespace WorthReads.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string FirstName,
                                string LastName,
                                string Email,
                                string Password) : IRequest<OneOf<UserResponse, IServiceError, ValidationErrors>>;
