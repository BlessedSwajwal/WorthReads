using WorthReads.Domain.Users;

namespace WorthReads.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    User AddUser(User user);
    User? GetUserByEmail(string email);

}
