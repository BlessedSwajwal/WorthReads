using Application.Common.Exceptions;
using Application.Common.Services;
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
    private readonly IJwtGenerator _jwtGenerator;

    public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository, IJwtGenerator jwtGenerator)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<OneOf<UserResponse, IServiceError, ValidationErrors>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        User existingUser = _userRepository.GetUserByEmail(request.Email)!;

        if (!existingUser.Equals(User.UserEmpty))
        {
            return new UserAlreadyExistsError();
        }

        var user = User.Create(request.FirstName, request.LastName, request.Email, request.Password);
        var savedUser = _userRepository.AddUser(user);
        //Generate Token
        var token = _jwtGenerator.GenerateJwt(savedUser);

        var userResponse = _mapper.From(savedUser).AddParameters("Token", token).AdaptToType<UserResponse>();
        return userResponse;
    }
}
