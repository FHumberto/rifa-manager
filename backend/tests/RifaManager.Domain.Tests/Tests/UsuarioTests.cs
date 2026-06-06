using RifaManager.Domain.Abstractions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Enums;
using RifaManager.Domain.Errors;
using Shouldly;

namespace RifaManager.Domain.Tests.Tests;

public sealed class UsuarioTests
{
    [Fact(DisplayName = "Deve criar usuario com dados validos")]
    public void Deve_criar_usuario_com_dados_validos()
    {
        Usuario usuario = new("Admin", "admin@rifa.com", "senha-hash", PerfilUsuario.Administrador, true);

        usuario.Id.ShouldNotBe(Guid.Empty);
        usuario.Nome.ShouldBe("Admin");
        usuario.Email.ShouldBe("admin@rifa.com");
        usuario.Perfil.ShouldBe(PerfilUsuario.Administrador);
        usuario.Ativo.ShouldBeTrue();
        usuario.BilhetesVendidos.ShouldBeEmpty();
    }

    [Fact(DisplayName = "Nao deve criar usuario sem nome")]
    public void Nao_deve_criar_usuario_sem_nome()
    {
        DomainException exception = Should.Throw<DomainException>(() =>
            new Usuario(string.Empty, "admin@rifa.com", "senha-hash", PerfilUsuario.Administrador, true));

        exception.Error.ShouldBe(UsuarioErrors.NomeObrigatorio);
    }

    [Fact(DisplayName = "Nao deve criar usuario sem email")]
    public void Nao_deve_criar_usuario_sem_email()
    {
        DomainException exception = Should.Throw<DomainException>(() =>
            new Usuario("Admin", string.Empty, "senha-hash", PerfilUsuario.Administrador, true));

        exception.Error.ShouldBe(UsuarioErrors.EmailObrigatorio);
    }

    [Fact(DisplayName = "Nao deve criar usuario com perfil invalido")]
    public void Nao_deve_criar_usuario_com_perfil_invalido()
    {
        DomainException exception = Should.Throw<DomainException>(() =>
            new Usuario("Admin", "admin@rifa.com", "senha-hash", (PerfilUsuario)999, true));

        exception.Error.ShouldBe(UsuarioErrors.PerfilInvalido);
    }

    [Fact(DisplayName = "Deve atualizar usuario com dados validos")]
    public void Deve_atualizar_usuario_com_dados_validos()
    {
        Usuario usuario = new("Admin", "admin@rifa.com", "senha-hash", PerfilUsuario.Administrador, true);

        usuario.Atualizar("Usuario", "usuario@rifa.com", PerfilUsuario.Comum);

        usuario.Nome.ShouldBe("Usuario");
        usuario.Email.ShouldBe("usuario@rifa.com");
        usuario.Perfil.ShouldBe(PerfilUsuario.Comum);
    }

    [Fact(DisplayName = "Nao deve atualizar usuario com nome invalido")]
    public void Nao_deve_atualizar_usuario_com_nome_invalido()
    {
        Usuario usuario = new("Admin", "admin@rifa.com", "senha-hash", PerfilUsuario.Administrador, true);

        DomainException exception = Should.Throw<DomainException>(() =>
            usuario.Atualizar(string.Empty, "admin@rifa.com", PerfilUsuario.Administrador));

        exception.Error.ShouldBe(UsuarioErrors.NomeObrigatorio);
    }

    [Fact(DisplayName = "Deve ativar usuario")]
    public void Deve_ativar_usuario()
    {
        Usuario usuario = new("Usuario", "usuario@rifa.com", "senha-hash", PerfilUsuario.Comum, false);

        usuario.Ativar();

        usuario.Ativo.ShouldBeTrue();
    }

    [Fact(DisplayName = "Deve desativar usuario")]
    public void Deve_desativar_usuario()
    {
        Usuario usuario = new("Usuario", "usuario@rifa.com", "senha-hash", PerfilUsuario.Comum, true);

        usuario.Desativar();

        usuario.Ativo.ShouldBeFalse();
    }

    [Fact(DisplayName = "Deve permitir acesso quando usuario estiver ativo")]
    public void Deve_permitir_acesso_quando_usuario_estiver_ativo()
    {
        Usuario usuario = new("Usuario", "usuario@rifa.com", "senha-hash", PerfilUsuario.Comum, true);

        usuario.PodeAcessarSistema().ShouldBeTrue();
    }

    [Fact(DisplayName = "Nao deve permitir acesso quando usuario estiver inativo")]
    public void Nao_deve_permitir_acesso_quando_usuario_estiver_inativo()
    {
        Usuario usuario = new("Usuario", "usuario@rifa.com", "senha-hash", PerfilUsuario.Comum, false);

        usuario.PodeAcessarSistema().ShouldBeFalse();
    }

    [Fact(DisplayName = "Deve validar acesso do usuario ativo")]
    public void Deve_validar_acesso_do_usuario_ativo()
    {
        Usuario usuario = new("Usuario", "usuario@rifa.com", "senha-hash", PerfilUsuario.Comum, true);

        Should.NotThrow(usuario.ValidarAcessoAoSistema);
    }

    [Fact(DisplayName = "Nao deve validar acesso do usuario inativo")]
    public void Nao_deve_validar_acesso_do_usuario_inativo()
    {
        Usuario usuario = new("Usuario", "usuario@rifa.com", "senha-hash", PerfilUsuario.Comum, false);

        DomainException exception = Should.Throw<DomainException>(usuario.ValidarAcessoAoSistema);

        exception.Error.ShouldBe(UsuarioErrors.InativoNaoPodeAcessarSistema);
    }

    [Fact(DisplayName = "Deve permitir gerenciar usuarios quando for administrador ativo")]
    public void Deve_permitir_gerenciar_usuarios_quando_for_administrador_ativo()
    {
        Usuario usuario = new("Admin", "admin@rifa.com", "senha-hash", PerfilUsuario.Administrador, true);

        usuario.PodeGerenciarUsuarios().ShouldBeTrue();
    }

    [Fact(DisplayName = "Nao deve permitir gerenciar usuarios quando for usuario comum")]
    public void Nao_deve_permitir_gerenciar_usuarios_quando_for_usuario_comum()
    {
        Usuario usuario = new("Usuario", "usuario@rifa.com", "senha-hash", PerfilUsuario.Comum, true);

        usuario.PodeGerenciarUsuarios().ShouldBeFalse();
    }

    [Fact(DisplayName = "Nao deve permitir gerenciar usuarios quando administrador estiver inativo")]
    public void Nao_deve_permitir_gerenciar_usuarios_quando_administrador_estiver_inativo()
    {
        Usuario usuario = new("Admin", "admin@rifa.com", "senha-hash", PerfilUsuario.Administrador, false);

        usuario.PodeGerenciarUsuarios().ShouldBeFalse();
    }

    [Fact(DisplayName = "Deve validar permissao para administrador ativo gerenciar usuarios")]
    public void Deve_validar_permissao_para_administrador_ativo_gerenciar_usuarios()
    {
        Usuario usuario = new("Admin", "admin@rifa.com", "senha-hash", PerfilUsuario.Administrador, true);

        Should.NotThrow(usuario.ValidarPermissaoParaGerenciarUsuarios);
    }

    [Fact(DisplayName = "Nao deve validar permissao para usuario sem permissao gerenciar usuarios")]
    public void Nao_deve_validar_permissao_para_usuario_sem_permissao_gerenciar_usuarios()
    {
        Usuario usuario = new("Usuario", "usuario@rifa.com", "senha-hash", PerfilUsuario.Comum, true);

        DomainException exception = Should.Throw<DomainException>(usuario.ValidarPermissaoParaGerenciarUsuarios);

        exception.Error.ShouldBe(UsuarioErrors.SemPermissaoParaGerenciarUsuarios);
    }
}
