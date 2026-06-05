using Microsoft.Extensions.DependencyInjection;
using RifaManager.Application.Features.Cadastrar;
using RifaManager.Application.Features.GetById;

namespace RifaManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddFeatures();

        return services;
    }

    private static void AddFeatures(this IServiceCollection services)
    {
        services.AddScoped<IGetRifaByIdUseCase, GetRifaByIdHandler>();
        services.AddScoped<ICadastrarRifaUseCase, CadastrarRifaHandler>();
    }
}
