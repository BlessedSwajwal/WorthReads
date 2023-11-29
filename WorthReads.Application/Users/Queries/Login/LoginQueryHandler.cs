using Application.Common.Interfaces.Repositories;
using Application.Common.Services;
using MapsterMapper;
using MediatR;
using OneOf;
using WorthReads.Application.Common.Exceptions;
using WorthReads.Application.Common.Exceptions.ValidationException;
using WorthReads.Application.Users.Common;
using WorthReads.Domain.Users;

namespace WorthReads.Application.Users.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, OneOf<UserResponse, IServiceError, ValidationErrors>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginQueryHandler(IMapper mapper, IJwtGenerator jwtGenerator, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _jwtGenerator = jwtGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<OneOf<UserResponse, IServiceError, ValidationErrors>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        User user = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email)!;
        if (user.Equals(User.UserEmpty) || user.Password != request.Password) return new InvalidCredentialsError();

        //Generate token
        var token = _jwtGenerator.GenerateJwt(user);


        UserResponse userResponse = _mapper.From(user).AddParameters("Token", token).AdaptToType<UserResponse>();
        return userResponse;
    }
}
