using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using WorthReads.Application.Common.Interfaces.Repositories;
using WorthReads.Domain.Users;

namespace Infrastructure.Persistence.Repositories.EfRepository;

public class UserRepository : IUserRepository
{
    private readonly WorthReadsDbContext _context;

    public UserRepository(WorthReadsDbContext context)
    {
        _context = context;
    }

    public async Task<User> AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        return user;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user is null) return User.UserEmpty;
        return user;
    }
}
