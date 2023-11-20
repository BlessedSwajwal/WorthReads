using Mapster;
using WorthReads.Application.Users.Commands.CreateUser;
using WorthReads.Contracts;

namespace WorthReads.API.Mapping;

public class CreateUserMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserRequest, CreateUserCommand>()
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Password, src => src.Password);
    }
}
