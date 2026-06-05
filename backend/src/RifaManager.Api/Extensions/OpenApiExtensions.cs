using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;

namespace RifaManager.Api.Extensions;

public static class OpenApiExtensions
{
    #region [ EXTENSOES ]

    public static IServiceCollection AddOpenApiDocs(this IServiceCollection services)
    {
        //? Adiciona um documento OpenAPI para cada versao de API declarada usando os provedores de versao encontrados no assembly.
        foreach (string documentName in GetDeclaredApiVersionDocuments())
        {
            OpenApiServiceCollectionExtensions.AddOpenApi(services, documentName, _ => { });
        }

        services.ConfigureOptions<ConfigureOpenApiOptions>();

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    public static WebApplication MapScalarDocs(this WebApplication app)
    {
        IApiVersionDescriptionProvider provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        string[] documents = provider.ApiVersionDescriptions.Select(description => description.GroupName)
                                                            .ToArray();

        app.MapScalarApiReference(options =>
        {
            options.WithTitle("Rifa Manager API")
                   .AddDocuments(documents)
                   .WithTheme(ScalarTheme.Default)
                   .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
                   .WithOpenApiRoutePattern("/openapi/{documentName}.json")
                   .SortTagsAlphabetically()
                   .SortOperationsByMethod()
                   .ExpandAllTags()
                   .HideDeveloperTools();
        });

        return app;
    }

    public static WebApplication UseScalarDocs(this WebApplication app)
    {
        if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
        {
            app.MapOpenApi();
            app.MapScalarDocs();
        }

        return app;
    }

    #endregion

    #region [ AUXILIARES ]

    private static IEnumerable<string> GetDeclaredApiVersionDocuments()
    {
        return typeof(OpenApiExtensions).Assembly
            .GetTypes()
            .SelectMany(type => type.GetCustomAttributes(inherit: true).OfType<IApiVersionProvider>())
            .SelectMany(provider => provider.Versions)
            .Select(version => $"v{version:VVV}")
            .Distinct()
            .Order();
    }

    private sealed class ConfigureOpenApiOptions(IApiVersionDescriptionProvider provider) : IConfigureNamedOptions<OpenApiOptions>
    {
        public void Configure(OpenApiOptions options) => Configure(Options.DefaultName, options);

        public void Configure(string? name, OpenApiOptions options)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            ApiVersionDescription? description = provider.ApiVersionDescriptions.FirstOrDefault(description => description.GroupName == name);

            if (description is null)
                return;

            options.ShouldInclude = apiDescription =>
                string.IsNullOrWhiteSpace(apiDescription.GroupName) ||
                apiDescription.GroupName == description.GroupName;
            options.AddDocumentTransformer((document, _, _) =>
            {
                document.Info.Title = "Rifa Manager API";
                document.Info.Version = description.ApiVersion.ToString();

                return Task.CompletedTask;
            });
        }
    }

    #endregion
}
