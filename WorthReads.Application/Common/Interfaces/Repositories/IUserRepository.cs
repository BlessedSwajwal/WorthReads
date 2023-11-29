using WorthReads.Domain.Users;

namespace WorthReads.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> AddUserAsync(User user);
    Task<User> GetUserByEmailAsync(string email);

}
