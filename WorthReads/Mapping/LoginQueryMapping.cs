using Mapster;
using WorthReads.Application.Users.Queries.Login;
using WorthReads.Contracts;

namespace WorthReads.API.Mapping;

public class LoginQueryMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginRequest, LoginQuery>()
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Password, src => src.Password);
    }
}
