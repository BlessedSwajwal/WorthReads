using Domain.PdfContainer;
using WorthReads.Domain.Common.Models;
using WorthReads.Domain.Users.ValueObjects;

namespace WorthReads.Domain.Users;

public class User : Entity<UserId>
{
    public static readonly User UserEmpty = new() { Id = UserId.Create(Guid.Empty) };
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    private List<PdfContainerId> _pdfs = new List<PdfContainerId>();
    public IReadOnlyList<PdfContainerId> OwningPdfs => _pdfs.AsReadOnly();

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

    public void AddPdfContainer(PdfContainerId id)
    {
        _pdfs.Add(id);
    }

    public void RemovePdf(PdfContainerId id)
    {
        _pdfs.Remove(id);
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
