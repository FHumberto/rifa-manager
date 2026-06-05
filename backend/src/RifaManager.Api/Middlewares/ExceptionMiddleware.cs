using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace RifaManager.Api.Middlewares;

public sealed class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger) : IExceptionHandler
{
    #region [ HANDLER ]

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ProblemDetails problem = new();

        switch (exception)
        {
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