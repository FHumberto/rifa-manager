using System.Net;

namespace RifaManager.Web.Extensions;

public static class HttpResponseMessageExtensions
{
    public static bool IsUnauthorized(this HttpResponseMessage response) => response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden;
}
