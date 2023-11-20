using Mapster;
using WorthReads.Application.Users.Common;
using WorthReads.Domain.Users;

namespace WorthReads.Application.Common.Mapping;

public class UserResponseMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName);
    }
}
