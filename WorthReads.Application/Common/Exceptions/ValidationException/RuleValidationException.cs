
using FluentValidation.Results;
using System.Net;

namespace WorthReads.Application.Common.Exceptions.ValidationException;

public class RuleValidationException : Exception, IValidationException
{
    private readonly ValidationResult _validationResult;
    List<String> _errors;
    string BigError = "";
    public RuleValidationException(ValidationResult validationResult)
    {
        _validationResult = validationResult;
        _errors = (from _val in _validationResult.Errors
                   select _val.ErrorMessage).ToList();
    }

    public int StatusCode => (int)HttpStatusCode.BadRequest;


    public List<string> Errors { get => _errors; }

    public string ErrorDetails()
    {
        foreach (string error in _errors)
        {
            BigError += $"{error}\n";
        }
        return BigError;
    }
}