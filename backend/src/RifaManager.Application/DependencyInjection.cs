using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RifaManager.Application.UseCases.Login;
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
    }
}
