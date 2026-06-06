using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RifaManager.Application.UseCases.AtivarUsuario;
using RifaManager.Application.UseCases.DesativarUsuario;
using RifaManager.Application.UseCases.EditarUsuario;
using RifaManager.Application.UseCases.Login;
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
        services.AddScoped<ICadastrarUsuarioUseCase, CadastrarUsuarioUseCaseHandler>();
        services.AddScoped<IEditarUsuarioUseCase, EditarUsuarioUseCaseHandler>();
        services.AddScoped<IAtivarUsuarioUseCase, AtivarUsuarioUseCaseHandler>();
        services.AddScoped<IDesativarUsuarioUseCase, DesativarUsuarioUseCaseHandler>();
    }
}
