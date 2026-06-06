using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RifaManager.Domain.Persistence;
using RifaManager.Domain.Security.Cryptography;
using RifaManager.Domain.Security.Tokens;
using RifaManager.Infrastructure.Context;
using RifaManager.Infrastructure.Persistence;
using RifaManager.Infrastructure.Security.Cryptography;
using RifaManager.Infrastructure.Security.Tokens;

namespace RifaManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RifaDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IPasswordEncripter, PasswordEncripter>();
        services.AddScoped<IAccessTokenGenerator, JwtAccessTokenGenerator>();

        return services;
    }
}
