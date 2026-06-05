using Microsoft.Extensions.DependencyInjection;
using RifaManager.Application.Features.Atualizar;
using RifaManager.Application.Features.Cadastrar;
using RifaManager.Application.Features.Encerrar;
using RifaManager.Application.Features.GetById;
using RifaManager.Application.Features.Listar;

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
        services.AddScoped<IListarRifasUseCase, ListarRifasHandler>();
        services.AddScoped<IGetRifaByIdUseCase, GetRifaByIdHandler>();
        services.AddScoped<ICadastrarRifaUseCase, CadastrarRifaHandler>();
        services.AddScoped<IAtualizarRifaUseCase, AtualizarRifaHandler>();
        services.AddScoped<IEncerrarRifaUseCase, EncerrarRifaHandler>();
    }
}
