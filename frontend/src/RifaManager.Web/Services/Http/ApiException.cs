namespace RifaManager.Web.Services.Http;

public sealed class ApiException : Exception
{
    public ApiException(string message, int? statusCode = null) : base(message) => StatusCode = statusCode;

    public int? StatusCode { get; }
}
