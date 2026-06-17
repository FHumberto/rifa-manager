using Microsoft.Extensions.Options;
using RifaManager.Web.Services.Http;

namespace RifaManager.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiClientOptions>(configuration.GetSection("Api"));
        services.AddScoped(sp =>
        {
            ApiClientOptions options = sp.GetRequiredService<IOptions<ApiClientOptions>>().Value;
            return new HttpClient { BaseAddress = new Uri(options.BaseUrl) };
        });

        return services;
    }
}
