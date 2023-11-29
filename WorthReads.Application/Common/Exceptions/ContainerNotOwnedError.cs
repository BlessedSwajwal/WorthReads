using System.Net;
using WorthReads.Application.Common.Exceptions;

namespace Application.Common.Exceptions;

public class ContainerNotOwnedError : IServiceError
{
    public int StatusCode => (int)HttpStatusCode.Unauthorized;

    public string? ErrorMessage => "User is not owner of the container.";
}
