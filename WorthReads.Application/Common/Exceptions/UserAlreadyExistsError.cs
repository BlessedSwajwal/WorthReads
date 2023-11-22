using System.Net;
using WorthReads.Application.Common.Exceptions;

namespace Application.Common.Exceptions;

public class UserAlreadyExistsError : IServiceError
{
    public int StatusCode => (int)HttpStatusCode.Forbidden;

    public string? ErrorMessage => "Email already used.";
}
