using WorthReads.Application.Common.Interfaces.Repositories;
using WorthReads.Domain.Users;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private static List<User> _users = new List<User>();
    public async Task<User> AddUserAsync(User user)
    {
        _users.Add(user);
        return user;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        User? user = _users.FirstOrDefault(x => x.Email == email);
        if (user is null) return User.UserEmpty;
        return user;
    }
}
