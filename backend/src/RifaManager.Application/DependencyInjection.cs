using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RifaManager.Application.UseCases.Login;

namespace RifaManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<ILoginUseCase, LoginUseCaseHandler>();

        return services;
    }
}
