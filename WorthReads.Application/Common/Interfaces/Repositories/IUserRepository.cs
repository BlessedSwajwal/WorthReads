﻿using WorthReads.Domain.Users;
using WorthReads.Domain.Users.ValueObjects;

namespace WorthReads.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> AddUserAsync(User user);
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByIdAsync(UserId userId);

}
