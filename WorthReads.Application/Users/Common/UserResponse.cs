namespace WorthReads.Application.Users.Common;

public record UserResponse
(
    Guid Id,
     string FirstName,
     string LastName,
     string Email,
     string Token
);
