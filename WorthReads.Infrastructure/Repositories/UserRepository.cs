using WorthReads.Application.Common.Interfaces.Repositories;
using WorthReads.Domain.Users;

namespace WorthReads.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private static List<User> _users = new List<User>();
    public User AddUser(User user)
    {
        _users.Add(user);
        return user;
    }

    public User? GetUserByEmail(string email)
    {
        return _users.FirstOrDefault(x => x.Email == email);
    }
}
