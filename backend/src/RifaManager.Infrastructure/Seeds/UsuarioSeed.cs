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
            Senha = "AQAAAAIAAYagAAAAEDOSuz2SoafpO/IZGEl9L9YYNI5ojnfT6KKEOmRTr8EYiJ/fOQvuCnJ8ilXHJy4sLw==",
            Ativo = true
        },
        new
        {
            Id = UsuarioComumId,
            Nome = "Usuario Comum",
            Email = "usuario@rifamanager.local",
            Perfil = PerfilUsuario.Comum,
            Senha = "AQAAAAIAAYagAAAAEEy7ZCvRrfkE6u4+/ro1PagYUSkzb9aMHckterhHYCI2uThrCeoQrQLbCTtRK4Q8Zg==",
            Ativo = true
        }
    ];
}
