using RifaManager.Api;
using RifaManager.Api.Extensions;
using RifaManager.Application;
using RifaManager.Infrastructure;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddApi(builder.Configuration, builder.Environment);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Rifa Manager API")
               .WithTheme(ScalarTheme.Default)
               .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
               .WithOpenApiRoutePattern("/openapi/{documentName}.json")
               .SortTagsAlphabetically()
               .SortOperationsByMethod()
               .ExpandAllTags()
               .HideDeveloperTools();
    });
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseCorsPolicy();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
