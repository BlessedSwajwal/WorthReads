using Application.Common.Services;
using MapsterMapper;
using MediatR;
using OneOf;
using WorthReads.Application.Common.Exceptions;
using WorthReads.Application.Common.Exceptions.ValidationException;
using WorthReads.Application.Common.Interfaces.Repositories;
using WorthReads.Application.Users.Common;
using WorthReads.Domain.Users;

namespace WorthReads.Application.Users.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, OneOf<UserResponse, IServiceError, ValidationErrors>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginQueryHandler(IMapper mapper, IUserRepository userRepository, IJwtGenerator jwtGenerator)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<OneOf<UserResponse, IServiceError, ValidationErrors>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        User user = _userRepository.GetUserByEmail(request.Email)!;
        if (user.Equals(User.UserEmpty) || user.Password != request.Password) return new InvalidCredentialsError();

        //Generate token
        var token = _jwtGenerator.GenerateJwt(user);


        UserResponse userResponse = _mapper.From(user).AddParameters("Token", token).AdaptToType<UserResponse>();
        return userResponse;
    }
}
