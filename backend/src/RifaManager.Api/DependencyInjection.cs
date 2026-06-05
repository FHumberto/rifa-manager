using RifaManager.Api.Extensions;
using RifaManager.Api.Middlewares;

namespace RifaManager.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddCorsPolicies(configuration, environment);
        services.AddProblemDetails();
        services.AddExceptionHandler<ExceptionMiddleware>();
        services.AddControllers();
        services.AddOpenApi();

        return services;
    }
}
