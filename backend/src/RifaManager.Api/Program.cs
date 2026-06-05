using RifaManager.Api;
using RifaManager.Api.Extensions;
using RifaManager.Application;
using RifaManager.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddApi(builder.Configuration, builder.Environment);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();

app.UseScalarDocs();
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseCorsPolicy();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
