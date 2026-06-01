using RifaManager.Domain.Abstractions;
using RifaManager.Domain.Enums;
using RifaManager.Domain.Policies;

namespace RifaManager.Domain.Entities;

public sealed class Usuario : Entity
{
    #region [ PROPRIEDADES ]

    public string Nome { get; private set; }
    public string Email { get; private set; }
    public PerfilUsuario Perfil { get; private set; }
    public bool Ativo { get; private set; }

    public List<Bilhete> BilhetesVendidos { get; private set; } = [];

    #endregion

    #region [ CONSTRUTORES ]

    private Usuario()
    {
        Nome = string.Empty;
        Email = string.Empty;
    }

    public Usuario(string nome, string email, PerfilUsuario perfil, bool ativo)
    {
        Nome = nome;
        Email = email;
        Perfil = perfil;
        Ativo = ativo;

        IsValid();
    }

    #endregion

    #region [ VALIDACOES ]

    public override void IsValid()
    {
        if (string.IsNullOrWhiteSpace(Nome))
            throw new ArgumentException("O nome do usuário é obrigatório.");

        if (string.IsNullOrWhiteSpace(Email))
            throw new ArgumentException("O e-mail do usuário é obrigatório.");

        if (!Enum.IsDefined(Perfil))
            throw new ArgumentException("O perfil do usuário é inválido.");
    }

    #endregion

    #region [ COMORTAMENTO ]

    public void Atualizar(string nome, string email, PerfilUsuario perfil)
    {
        Nome = nome;
        Email = email;
        Perfil = perfil;

        IsValid();
    }

    public void Ativar() => Ativo = true;

    public void Desativar() => Ativo = false;

    public bool PodeAcessarSistema() => PoliticaAcesso.PodeAcessarSistema(this);

    public void ValidarAcessoAoSistema() => PoliticaAcesso.ValidarAcessoAoSistema(this);

    public bool PodeGerenciarUsuarios() => PoliticaAdministrativa.PodeGerenciarUsuarios(this);

    public void ValidarPermissaoParaGerenciarUsuarios() => PoliticaAdministrativa.ValidarPermissaoParaGerenciarUsuarios(this);

    #endregion

}
