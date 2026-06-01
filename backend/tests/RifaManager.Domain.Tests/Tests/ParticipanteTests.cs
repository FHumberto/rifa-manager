using RifaManager.Domain.Abstractions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using Shouldly;

namespace RifaManager.Domain.Tests.Tests;

public sealed class ParticipanteTests
{
    [Fact(DisplayName = "Deve criar participante com dados validos")]
    public void Deve_criar_participante_com_dados_validos()
    {
        Participante participante = new("Joao", "84999999999", "Observacao");

        participante.Id.ShouldNotBe(Guid.Empty);
        participante.Nome.ShouldBe("Joao");
        participante.Telefone.ShouldBe("84999999999");
        participante.Observacao.ShouldBe("Observacao");
        participante.Bilhetes.ShouldBeEmpty();
    }

    [Fact(DisplayName = "Deve criar participante sem observacao")]
    public void Deve_criar_participante_sem_observacao()
    {
        Participante participante = new("Joao", "84999999999", null);

        participante.Observacao.ShouldBeNull();
    }

    [Fact(DisplayName = "Nao deve criar participante sem nome")]
    public void Nao_deve_criar_participante_sem_nome()
    {
        DomainException exception = Should.Throw<DomainException>(() =>
            new Participante(string.Empty, "84999999999", null));

        exception.Error.ShouldBe(ParticipanteErrors.NomeObrigatorio);
    }

    [Fact(DisplayName = "Nao deve criar participante sem telefone")]
    public void Nao_deve_criar_participante_sem_telefone()
    {
        DomainException exception = Should.Throw<DomainException>(() =>
            new Participante("Joao", string.Empty, null));

        exception.Error.ShouldBe(ParticipanteErrors.TelefoneObrigatorio);
    }

    [Fact(DisplayName = "Deve atualizar participante com dados validos")]
    public void Deve_atualizar_participante_com_dados_validos()
    {
        Participante participante = new("Joao", "84999999999", null);

        participante.Atualizar("Maria", "84888888888", "Pago por pix");

        participante.Nome.ShouldBe("Maria");
        participante.Telefone.ShouldBe("84888888888");
        participante.Observacao.ShouldBe("Pago por pix");
    }

    [Fact(DisplayName = "Nao deve atualizar participante com nome invalido")]
    public void Nao_deve_atualizar_participante_com_nome_invalido()
    {
        Participante participante = new("Joao", "84999999999", null);

        DomainException exception = Should.Throw<DomainException>(() =>
            participante.Atualizar(string.Empty, "84999999999", null));

        exception.Error.ShouldBe(ParticipanteErrors.NomeObrigatorio);
    }

    [Fact(DisplayName = "Nao deve atualizar participante com telefone invalido")]
    public void Nao_deve_atualizar_participante_com_telefone_invalido()
    {
        Participante participante = new("Joao", "84999999999", null);

        DomainException exception = Should.Throw<DomainException>(() =>
            participante.Atualizar("Joao", string.Empty, null));

        exception.Error.ShouldBe(ParticipanteErrors.TelefoneObrigatorio);
    }
}
