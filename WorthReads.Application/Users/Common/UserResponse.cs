namespace WorthReads.Application.Users.Common;

public class UserResponse
{
    public Guid Id;
    public string FirstName = null!;
    public string LastName = null!;
    public string Email = null!;
    public string? Token;
}
