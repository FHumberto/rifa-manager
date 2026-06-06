using RifaManager.Domain.Abstractions;

namespace RifaManager.Domain.Errors;

public static class UsuarioErrors
{
    public static readonly Error NomeObrigatorio =
        Error.Validation("Usuario.NomeObrigatorio", "O nome do usuário é obrigatório.");

    public static readonly Error NomeMuitoLongo =
        Error.Validation("Usuario.NomeMuitoLongo", "O nome do usuário deve conter no máximo 100 caracteres.");

    public static readonly Error EmailObrigatorio =
        Error.Validation("Usuario.EmailObrigatorio", "O e-mail do usuário é obrigatório.");

    public static readonly Error EmailMuitoLongo =
        Error.Validation("Usuario.EmailMuitoLongo", "O e-mail do usuário deve conter no máximo 100 caracteres.");

    public static readonly Error SenhaObrigatoria =
        Error.Validation("Usuario.SenhaObrigatoria", "A senha do usuário é obrigatória.");

    public static readonly Error SenhaMuitoLonga =
        Error.Validation("Usuario.SenhaMuitoLonga", "A senha do usuário deve conter no máximo 200 caracteres.");

    public static readonly Error PerfilInvalido =
        Error.Validation("Usuario.PerfilInvalido", "O perfil do usuário é inválido.");

    public static readonly Error InativoNaoPodeAcessarSistema =
        Error.AccessUnauthorized("Usuario.InativoNaoPodeAcessarSistema", "Usuário inativo não pode acessar o sistema.");

    public static readonly Error SemPermissaoParaGerenciarUsuarios =
        Error.AccessForbidden("Usuario.SemPermissaoParaGerenciarUsuarios", "Somente administradores ativos podem gerenciar usuários.");

    public static readonly Error UsuarioSenhaInvalida =
        Error.Validation("Usuario.UsuarioSenhaInvalida", "Email ou senha inválidos.");
}
