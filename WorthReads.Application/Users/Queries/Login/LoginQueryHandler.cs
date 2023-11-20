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

    public LoginQueryHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<OneOf<UserResponse, IServiceError, ValidationErrors>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        User? user = _userRepository.GetUserByEmail(request.Email);
        if (user is null || user.Password != request.Password) return new InvalidCredentialsError();

        UserResponse userResponse = _mapper.Map<UserResponse>(user);
        return userResponse;
    }
}
