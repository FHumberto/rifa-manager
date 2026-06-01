using RifaManager.Domain.Entities;

namespace RifaManager.Domain.Policies;

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
            throw new UnauthorizedAccessException("Usuario inativo nao pode acessar o sistema.");
    }
}
