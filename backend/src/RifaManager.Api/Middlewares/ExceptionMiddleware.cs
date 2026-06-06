using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RifaManager.Application.Exceptions;
using RifaManager.Domain.Abstractions;

namespace RifaManager.Api.Middlewares;

public sealed class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger) : IExceptionHandler
{
    #region [ HANDLER ]

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ProblemDetails problem = new();

        switch (exception)
        {

            case NotFoundException notFoundException:
                problem.Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-404-not-found";
                problem.Status = StatusCodes.Status404NotFound;
                problem.Title = "Recurso não encontrado.";
                SetDetailIfExists(problem, notFoundException.Message);
                break;

            case BadRequestException badRequestException:
                problem.Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-400-bad-request";
                problem.Status = StatusCodes.Status400BadRequest;
                problem.Title = "Requisição inválida.";
                if (badRequestException.ValidationErrors is null)
                    SetDetailIfExists(problem, badRequestException.Message);

                SetValidationErrorsIfExists(problem, badRequestException.ValidationErrors);
                break;

            case UnauthorizedException unauthorizedException:
                problem.Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-401-unauthorized";
                problem.Status = StatusCodes.Status401Unauthorized;
                problem.Title = "Nao autorizado.";
                SetDetailIfExists(problem, unauthorizedException.Message);
                break;

            case DomainException domainException:
                problem.Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-400-bad-request";
                problem.Status = StatusCodes.Status400BadRequest;
                problem.Title = "Requisição inválida.";
                SetDetailIfExists(problem, domainException.Error.Description);
                break;

            case NotImplementedException:
                problem.Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-501-not-implemented";
                problem.Status = StatusCodes.Status501NotImplemented;
                problem.Title = "Este recurso ainda não está disponível.";
                break;

            default:
                problem.Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-500-internal-server-error";
                problem.Status = StatusCodes.Status500InternalServerError;
                problem.Title = "Erro Interno do Servidor. Por favor, tente novamente mais tarde.";
                LogUnhandledException(httpContext, exception);
                break;
        }

        httpContext.Response.StatusCode = problem.Status ?? StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = MediaTypeNames.Application.ProblemJson;

        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
        return true;
    }

    #endregion

    #region [ METODOS AUXILIARES ]

    //? A RFC espera que o campo 'detail' seja uma string descritiva do erro.
    private static void SetDetailIfExists(ProblemDetails problem, string? message)
    {
        if (!string.IsNullOrWhiteSpace(message))
        {
            problem.Detail = message;
        }
    }

    //? O dicionário entra como extensão.
    private static void SetValidationErrorsIfExists(ProblemDetails problem, IReadOnlyDictionary<string, string[]>? validationErrors)
    {
        if (validationErrors?.Count > 0)
        {
            problem.Extensions["errors"] = validationErrors;
        }
    }

    private void LogUnhandledException(HttpContext context, Exception exception)
    {
        //? Log apenas se o nível de erro estiver habilitado.
        if (!logger.IsEnabled(LogLevel.Error))
            return;

        logger.LogError
        (
            exception,
            "Tipo={ExceptionType} Msg={ExceptionMessage} Verbo={Method} Caminho={Path} TraceId={TraceId}",
            exception.GetType().FullName,
            exception.Message,
            context.Request.Method,
            context.Request.Path,
            context.TraceIdentifier
        );
    }

    #endregion
}
