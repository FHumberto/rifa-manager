using RifaManager.Domain.Abstractions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Enums;
using RifaManager.Domain.Errors;
using Shouldly;
using static RifaManager.Domain.Tests.Factories.EntityTestFactory;

namespace RifaManager.Domain.Tests.Tests;

public sealed class BilheteTests
{
    [Fact(DisplayName = "Deve criar bilhete com dados validos")]
    public void Deve_criar_bilhete_com_dados_validos()
    {
        Rifa rifa = CriarRifa();
        Participante participante = CriarParticipante();
        Usuario usuario = CriarAdministrador();

        Bilhete bilhete = new(1, rifa, participante, usuario);

        bilhete.Id.ShouldNotBe(Guid.Empty);
        bilhete.Numero.ShouldBe(1);
        bilhete.Status.ShouldBe(StatusPagamento.Pendente);
        bilhete.CriadoEm.ShouldNotBe(default);
        bilhete.PagoEm.ShouldBe(default);
        bilhete.CanceladoEm.ShouldBe(default);
    }

    [Fact(DisplayName = "Deve criar referencias e propriedades de navegacao do bilhete")]
    public void Deve_criar_referencias_e_propriedades_de_navegacao()
    {
        Rifa rifa = CriarRifa();
        Participante participante = CriarParticipante();
        Usuario usuario = CriarAdministrador();

        Bilhete bilhete = new(1, rifa, participante, usuario);

        bilhete.RifaId.ShouldBe(rifa.Id);
        bilhete.Rifa.ShouldBe(rifa);
        bilhete.ParticipanteId.ShouldBe(participante.Id);
        bilhete.Participante.ShouldBe(participante);
        bilhete.UsuarioResponsavelId.ShouldBe(usuario.Id);
        bilhete.UsuarioResponsavel.ShouldBe(usuario);
        rifa.Bilhetes.ShouldContain(bilhete);
        participante.Bilhetes.ShouldContain(bilhete);
        usuario.BilhetesVendidos.ShouldContain(bilhete);
    }

    [Theory(DisplayName = "Nao deve criar bilhete com numero invalido")]
    [InlineData(0)]
    [InlineData(-1)]
    public void Nao_deve_criar_bilhete_com_numero_invalido(int numero)
    {
        DomainException exception = Should.Throw<DomainException>(() =>
            new Bilhete(numero, CriarRifa(), CriarParticipante(), CriarAdministrador()));

        exception.Error.ShouldBe(BilheteErrors.NumeroInvalido);
    }

    [Fact(DisplayName = "Nao deve criar bilhete sem rifa")]
    public void Nao_deve_criar_bilhete_sem_rifa()
    {
        Should.Throw<ArgumentNullException>(() =>
            new Bilhete(1, null!, CriarParticipante(), CriarAdministrador()));
    }

    [Fact(DisplayName = "Nao deve criar bilhete sem participante")]
    public void Nao_deve_criar_bilhete_sem_participante()
    {
        Should.Throw<ArgumentNullException>(() =>
            new Bilhete(1, CriarRifa(), null!, CriarAdministrador()));
    }

    [Fact(DisplayName = "Nao deve criar bilhete sem usuario responsavel")]
    public void Nao_deve_criar_bilhete_sem_usuario_responsavel()
    {
        Should.Throw<ArgumentNullException>(() =>
            new Bilhete(1, CriarRifa(), CriarParticipante(), null!));
    }

    [Fact(DisplayName = "Deve marcar bilhete como pago pela rifa")]
    public void Deve_marcar_bilhete_como_pago_pela_rifa()
    {
        Rifa rifa = CriarRifa();
        Bilhete bilhete = CriarBilhete(rifa);

        rifa.MarcarBilheteComoPago(bilhete);

        bilhete.Status.ShouldBe(StatusPagamento.Pago);
        bilhete.PagoEm.ShouldNotBe(default);
    }

    [Fact(DisplayName = "Deve marcar bilhete como cancelado pela rifa")]
    public void Deve_marcar_bilhete_como_cancelado_pela_rifa()
    {
        Rifa rifa = CriarRifa();
        Bilhete bilhete = CriarBilhete(rifa);

        rifa.MarcarBilheteComoCancelado(bilhete);

        bilhete.Status.ShouldBe(StatusPagamento.Cancelado);
        bilhete.CanceladoEm.ShouldNotBe(default);
    }

    [Fact(DisplayName = "Nao deve marcar bilhete cancelado como pago")]
    public void Nao_deve_marcar_bilhete_cancelado_como_pago()
    {
        Rifa rifa = CriarRifa();
        Bilhete bilhete = CriarBilhete(rifa);
        rifa.MarcarBilheteComoCancelado(bilhete);

        DomainException exception = Should.Throw<DomainException>(() =>
            rifa.MarcarBilheteComoPago(bilhete));

        exception.Error.ShouldBe(BilheteErrors.CanceladoNaoPodeSerPago);
    }

    [Fact(DisplayName = "Nao deve marcar bilhete pago como cancelado")]
    public void Nao_deve_marcar_bilhete_pago_como_cancelado()
    {
        Rifa rifa = CriarRifa();
        Bilhete bilhete = CriarBilhete(rifa);
        rifa.MarcarBilheteComoPago(bilhete);

        DomainException exception = Should.Throw<DomainException>(() =>
            rifa.MarcarBilheteComoCancelado(bilhete));

        exception.Error.ShouldBe(BilheteErrors.PagoNaoPodeSerCancelado);
    }
}
