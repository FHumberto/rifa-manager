using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace RifaManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
