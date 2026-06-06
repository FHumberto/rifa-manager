using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RifaManager.Application.UseCases.Login;
using RifaManager.Application.UseCases.Rifas.CadastrarRifa;
using RifaManager.Application.UseCases.Rifas.EditarRifa;
using RifaManager.Application.UseCases.Rifas.EncerrarRifa;
using RifaManager.Application.UseCases.Rifas.GetById;
using RifaManager.Application.UseCases.Rifas.ListarRifas;
using RifaManager.Application.UseCases.Rifas.SortearRifa;
using RifaManager.Application.UseCases.Usuarios.AtivarUsuario;
using RifaManager.Application.UseCases.Usuarios.CadastrarUsuario;
using RifaManager.Application.UseCases.Usuarios.DesativarUsuario;
using RifaManager.Application.UseCases.Usuarios.EditarUsuario;
using RifaManager.Application.UseCases.Usuarios.GetById;

namespace RifaManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        services.AddUseCases();

        return services;
    }

    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ILoginUseCase, LoginUseCaseHandler>();
        services.AddScoped<IGetUsuarioByIdUseCase, GetUsuarioByIdHandler>();
        services.AddScoped<ICadastrarRifaUseCase, CadastrarRifaUseCaseHandler>();
        services.AddScoped<IEditarRifaUseCase, EditarRifaUseCaseHandler>();
        services.AddScoped<IEncerrarRifaUseCase, EncerrarRifaUseCaseHandler>();
        services.AddScoped<IGetRifaByIdUseCase, GetRifaByIdHandler>();
        services.AddScoped<IListarRifasUseCase, ListarRifasHandler>();
        services.AddScoped<ISortearRifaUseCase, SortearRifaUseCaseHandler>();
        services.AddScoped<ICadastrarUsuarioUseCase, CadastrarUsuarioUseCaseHandler>();
        services.AddScoped<IEditarUsuarioUseCase, EditarUsuarioUseCaseHandler>();
        services.AddScoped<IAtivarUsuarioUseCase, AtivarUsuarioUseCaseHandler>();
        services.AddScoped<IDesativarUsuarioUseCase, DesativarUsuarioUseCaseHandler>();
    }
}
