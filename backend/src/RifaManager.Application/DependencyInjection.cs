using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RifaManager.Application.Abstractions.Extensions;
using RifaManager.Application.Abstractions.Markers;
using RifaManager.Application.UseCases.Login;

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
        services.AddScopedByMarker<IUseCase>(typeof(ILoginUseCase).Assembly);
    }
}
