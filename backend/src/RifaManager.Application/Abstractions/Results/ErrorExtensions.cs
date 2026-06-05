using Ardalis.Result;
using RifaManager.Domain.Abstractions;

namespace RifaManager.Application.Abstractions.Results;

public static class ErrorExtensions
{
    public static Result<T> ToInvalidResult<T>(this Error error)
    {
        return Result<T>.Invalid(new ValidationError
        {
            Identifier = error.Code,
            ErrorMessage = error.Description
        });
    }
}
