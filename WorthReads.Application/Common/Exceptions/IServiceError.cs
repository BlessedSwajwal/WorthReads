namespace WorthReads.Application.Common.Exceptions;

public interface IServiceError
{
    public int StatusCode { get; }
    public string? ErrorMessage { get; }
}
