using Ardalis.Result;
using RifaManager.Domain.Abstractions;

namespace RifaManager.Application.Abstractions.Results;

public static class ErrorExtensions
{
    public static Result<T> ToResult<T>(this Error error)
    {
        return error.ErrorType switch
        {
            ErrorType.Validation => Result<T>.Invalid(new ValidationError(error.Code, error.Description)),
            ErrorType.NotFound => Result<T>.NotFound(error.Code, error.Description),
            ErrorType.Conflict => Result<T>.Conflict(error.Code, error.Description),
            _ => Result<T>.Error($"{error.Code}: {error.Description}")
        };
    }
}
