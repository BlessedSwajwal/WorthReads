using System.Net;
using WorthReads.Application.Common.Exceptions;

namespace Application.Common.Exceptions;

public class ContainerNotFoundError : IServiceError
{
    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string? ErrorMessage => "No countainer found with given ID.";
}
