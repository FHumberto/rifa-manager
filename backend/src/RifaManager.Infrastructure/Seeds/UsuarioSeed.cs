using RifaManager.Domain.Enums;

namespace RifaManager.Infrastructure.Seeds;

public static class UsuarioSeed
{
    public static readonly Guid AdministradorId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    public static readonly Guid UsuarioComumId = Guid.Parse("22222222-2222-2222-2222-222222222222");

    public static object[] Data =>
    [
        new
        {
            Id = AdministradorId,
            Nome = "Administrador",
            Email = "admin@rifamanager.local",
            Perfil = PerfilUsuario.Administrador,
            Senha = "pbkdf2_sha256$100000$cmlmYS1tYW5hZ2VyLWFkbWluLXNhbHQ=$Jwh4dZlB22rYR5uoofn97Y7FJwdnwOdI8KF8NzjssEM=",
            Ativo = true
        },
        new
        {
            Id = UsuarioComumId,
            Nome = "Usuario Comum",
            Email = "usuario@rifamanager.local",
            Perfil = PerfilUsuario.Comum,
            Senha = "pbkdf2_sha256$100000$cmlmYS1tYW5hZ2VyLXVzZXItc2FsdA==$+cik2bvSeHUEaI/Mp22vmzb9ZZ+KwN/p/F63xTDcWVg=",
            Ativo = true
        }
    ];
}
