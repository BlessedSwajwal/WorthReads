using System.Net;

namespace WorthReads.Application.Common.Exceptions;

public class InvalidCredentialsError : IServiceError
{
    public int StatusCode => (int)HttpStatusCode.NotFound;
    public string? ErrorMessage => "Invalid User Credentials.";
}
