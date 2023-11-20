namespace WorthReads.Application.Common.Exceptions.ValidationException;

public interface IValidationException
{
    int StatusCode { get; }
    List<String> Errors { get; }
}
