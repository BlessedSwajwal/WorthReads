using System.Net;
using WorthReads.Application.Common.Exceptions;

namespace Application.Common.Exceptions;

public class UserNotFoundError : IServiceError
{
    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string? ErrorMessage => "User not found";
}
