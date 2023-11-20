﻿using WorthReads.Domain.Common.Models;
using WorthReads.Domain.Users.ValueObjects;

namespace WorthReads.Domain.Users;

public class User : Entity<UserId>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    private User(UserId userId,
                 string firstName,
                 string lastName,
                 string email,
                 string password) : base(userId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public static User Create(string firstName, string lastName, string email, string password)
    {
        return new(
                UserId.CreateUnique(),
                firstName,
                lastName,
                email,
                password
            );
    }
#pragma warning disable CS8618
    private User() { }
}
