using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace RifaManager.Application.Abstractions.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddScopedByMarker<TMarker>(this IServiceCollection services, Assembly assembly)
    {
        Type markerType = typeof(TMarker);

        IEnumerable<Type> implementations = assembly.GetTypes()
                                                    .Where(type => type is { IsClass: true, IsAbstract: false });

        foreach (Type implementation in implementations)
        {
            Type? service = implementation.GetInterfaces()
                .FirstOrDefault(@interface => @interface != markerType && markerType.IsAssignableFrom(@interface));

            if (service is not null)
                services.AddScoped(service, implementation);
        }

        return services;
    }
}
