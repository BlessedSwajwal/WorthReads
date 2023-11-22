using WorthReads.Domain.Users;

namespace Application.Common.Services;

public interface IJwtGenerator
{
    string GenerateJwt(User user);
}
