using System.ComponentModel;
using System.Reflection;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using RifaManager.Domain.Abstractions;

namespace RifaManager.Api.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
public class BaseController : ControllerBase
{
    protected IActionResult Problem<T>(Result<T> result)
    {
        ErrorType errorType = ToErrorType(result.Status);
        int statusCode = (int)errorType;
        object problem = errorType is ErrorType.Validation
            ? ValidationProblem(result, errorType)
            : CreateProblemDetails(result, errorType);

        return StatusCode(statusCode, problem);
    }

    private ProblemDetails CreateProblemDetails<T>(Result<T> result, ErrorType errorType)
    {
        (string code, string description) = GetError(result, errorType);

        ProblemDetails problemDetails = new()
        {
            Type = ToRfcLink(errorType),
            Title = description,
            Detail = code,
            Status = (int)errorType
        };

        problemDetails.Extensions["traceId"] = HttpContext.TraceIdentifier;

        return problemDetails;
    }

    private ValidationProblemDetails ValidationProblem<T>(Result<T> result, ErrorType errorType)
    {
        Dictionary<string, string[]> errors = result.ValidationErrors
            .GroupBy(error => error.Identifier)
            .ToDictionary(group => group.Key, group => group.Select(error => error.ErrorMessage).ToArray());

        ValidationProblemDetails problemDetails = new(errors)
        {
            Type = ToRfcLink(errorType),
            Title = ToDescription(errorType),
            Detail = errorType.ToString(),
            Status = (int)errorType
        };

        problemDetails.Extensions["traceId"] = HttpContext.TraceIdentifier;

        return problemDetails;
    }

    private static ErrorType ToErrorType(ResultStatus status)
    {
        return status switch
        {
            ResultStatus.Invalid => ErrorType.Validation,
            ResultStatus.Unauthorized => ErrorType.AccessUnauthorized,
            ResultStatus.Forbidden => ErrorType.AccessForbidden,
            ResultStatus.NotFound => ErrorType.NotFound,
            ResultStatus.Conflict => ErrorType.Conflict,
            _ => ErrorType.Failure
        };
    }

    private static (string Code, string Description) GetError<T>(Result<T> result, ErrorType errorType)
    {
        string[] errors = result.Errors.ToArray();

        if (errors.Length >= 2)
            return (errors[0], errors[1]);

        if (errors.Length == 1)
            return (errorType.ToString(), errors[0]);

        return (errorType.ToString(), ToDescription(errorType));
    }

    private static string ToDescription(ErrorType errorType)
    {
        return errorType.GetType()
                        .GetField(errorType.ToString())!
                        .GetCustomAttribute<DescriptionAttribute>()!
                        .Description;
    }

    private static string ToRfcLink(ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.Validation => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.1",
            ErrorType.AccessUnauthorized => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.2",
            ErrorType.AccessForbidden => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.4",
            ErrorType.NotFound => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.5",
            ErrorType.Conflict => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.10",
            _ => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.6.1"
        };
    }
}
