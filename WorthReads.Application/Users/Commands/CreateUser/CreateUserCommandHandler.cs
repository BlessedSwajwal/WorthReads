using MapsterMapper;
using MediatR;
using OneOf;
using WorthReads.Application.Common.Exceptions;
using WorthReads.Application.Common.Exceptions.ValidationException;
using WorthReads.Application.Common.Interfaces.Repositories;
using WorthReads.Application.Users.Common;
using WorthReads.Domain.Users;

namespace WorthReads.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, OneOf<UserResponse, IServiceError, ValidationErrors>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<OneOf<UserResponse, IServiceError, ValidationErrors>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var user = User.Create(request.FirstName, request.LastName, request.Email, request.Password);
        var savedUser = _userRepository.AddUser(user);
        var userResponse = _mapper.Map<UserResponse>(savedUser);
        return userResponse;
    }
}
