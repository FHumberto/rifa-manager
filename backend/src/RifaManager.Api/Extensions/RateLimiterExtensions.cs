using System.Net.Mime;
using System.Security.Claims;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace RifaManager.Api.Extensions;

public static class RateLimiterExtensions
{
    private const int PermitLimit = 100;
    private static readonly TimeSpan s_window = TimeSpan.FromMinutes(1);

    public static IServiceCollection AddRateLimiterPolicies(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    GetPartitionKey(httpContext),
                    _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = PermitLimit,
                        Window = s_window,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0
                    }));

            options.OnRejected = async (context, cancellationToken) =>
            {
                HttpResponse response = context.HttpContext.Response;

                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out TimeSpan retryAfter))
                    response.Headers.RetryAfter = Math.Ceiling(retryAfter.TotalSeconds).ToString();

                response.StatusCode = StatusCodes.Status429TooManyRequests;
                response.ContentType = MediaTypeNames.Application.ProblemJson;

                await response.WriteAsJsonAsync(new ProblemDetails
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc6585#section-4",
                    Status = StatusCodes.Status429TooManyRequests,
                    Title = "Limite de requisicoes excedido.",
                    Detail = "Voce excedeu o limite de requisicoes. Tente novamente mais tarde."
                }, cancellationToken);
            };
        });

        return services;
    }

    private static string GetPartitionKey(HttpContext httpContext)
    {
        string? usuarioId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            ?? httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!string.IsNullOrWhiteSpace(usuarioId))
            return $"usuario:{usuarioId}";

        string? ip = httpContext.Connection.RemoteIpAddress?.ToString();
        return $"ip:{ip ?? "desconhecido"}";
    }
}
