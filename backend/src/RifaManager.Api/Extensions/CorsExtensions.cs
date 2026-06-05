using Microsoft.AspNetCore.Cors.Infrastructure;

namespace RifaManager.Api.Extensions;

public static class CorsExtensions
{
    private const string DEFAULT = "Padrao";
    private const string DEVELOP = "Desenvolvimento";

    #region [ EXTENSOES ]

    public static IServiceCollection AddCorsPolicies(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        string[]? allowedOrigins = GetAllowedOrigins(configuration);

        ValidateAllowedOrigins(environment, allowedOrigins);

        services.AddCors(options =>
        {
            AddDefaultPolicy(options, allowedOrigins);
            AddDevelopmentPolicy(options);
        });

        return services;
    }

    public static IApplicationBuilder UseCorsPolicy(this WebApplication app)
    {
        string? policyName = GetPolicyName(app.Environment);

        if (policyName is not null)
            app.UseCors(policyName);

        return app;
    }

    #endregion

    #region [ POLITICAS ]

    private static void AddDefaultPolicy(CorsOptions options, string[]? allowedOrigins)
    {
        if (allowedOrigins is not { Length: > 0 })
            return;

        options.AddPolicy(DEFAULT, builder =>
        {
            builder.WithOrigins(allowedOrigins)
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    }

    private static void AddDevelopmentPolicy(CorsOptions options)
    {
        options.AddPolicy(DEVELOP, builder
            => builder.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod());
    }

    #endregion

    #region [ AUXILIARES ]

    private static string GetPolicyName(IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
            return DEVELOP;

        return environment.IsStaging() || environment.IsProduction()
            ? DEFAULT
            : throw new InvalidOperationException($"Ambiente '{environment.EnvironmentName}' não suportado para configuração de CORS.");
    }

    private static string[]? GetAllowedOrigins(IConfiguration configuration)
    {
        return configuration.GetSection("CORS:AllowedOrigins")
                            .Get<string[]>();
    }

    private static void ValidateAllowedOrigins(IWebHostEnvironment environment, string[]? allowedOrigins)
    {
        if ((environment.IsStaging() || environment.IsProduction()) && allowedOrigins is not { Length: > 0 })
            throw new InvalidOperationException("CORS:AllowedOrigins deve ser configurada no ambiente de Homologação e Produção.");
    }

    #endregion
}
