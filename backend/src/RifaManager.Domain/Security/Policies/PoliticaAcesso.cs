using RifaManager.Domain.Abstractions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;

namespace RifaManager.Domain.Security.Policies;

public static class PoliticaAcesso
{
    public static bool PodeAcessarSistema(Usuario usuario)
    {
        ArgumentNullException.ThrowIfNull(usuario);

        return usuario.Ativo;
    }

    public static void ValidarAcessoAoSistema(Usuario usuario)
    {
        if (!PodeAcessarSistema(usuario))
            throw new DomainException(UsuarioErrors.InativoNaoPodeAcessarSistema);
    }
}
