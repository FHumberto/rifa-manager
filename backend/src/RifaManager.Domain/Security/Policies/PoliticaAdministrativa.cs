using RifaManager.Domain.Abstractions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Enums;
using RifaManager.Domain.Errors;

namespace RifaManager.Domain.Security.Policies;

public static class PoliticaAdministrativa
{
    public static bool PodeGerenciarUsuarios(Usuario usuario)
    {
        ArgumentNullException.ThrowIfNull(usuario);

        return usuario.Ativo && usuario.Perfil == PerfilUsuario.Administrador;
    }

    public static void ValidarPermissaoParaGerenciarUsuarios(Usuario usuario)
    {
        if (!PodeGerenciarUsuarios(usuario))
            throw new DomainException(UsuarioErrors.SemPermissaoParaGerenciarUsuarios);
    }
}
