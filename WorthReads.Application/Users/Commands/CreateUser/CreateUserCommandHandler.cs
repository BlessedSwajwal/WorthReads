using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Services;
using MapsterMapper;
using MediatR;
using OneOf;
using WorthReads.Application.Common.Exceptions;
using WorthReads.Application.Common.Exceptions.ValidationException;
using WorthReads.Application.Users.Common;
using WorthReads.Domain.Users;

namespace WorthReads.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, OneOf<UserResponse, IServiceError, ValidationErrors>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtGenerator _jwtGenerator;

    public CreateUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IJwtGenerator jwtGenerator)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<OneOf<UserResponse, IServiceError, ValidationErrors>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        User existingUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email)!;

        if (!existingUser.Equals(User.UserEmpty))
        {
            return new UserAlreadyExistsError();
        }

        //Hash the password
        var hashedPass = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = User.Create(request.FirstName, request.LastName, request.Email, hashedPass);
        var savedUser = await _unitOfWork.UserRepository.AddUserAsync(user);
        //Generate Token
        var token = _jwtGenerator.GenerateJwt(savedUser);

        var userResponse = _mapper.From(savedUser).AddParameters("Token", token).AdaptToType<UserResponse>();
        return userResponse;
    }
}
