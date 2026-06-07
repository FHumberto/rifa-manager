using RifaManager.Domain.Abstractions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Enums;
using RifaManager.Domain.Errors;
using Shouldly;
using static RifaManager.Domain.Tests.Factories.EntityTestFactory;

namespace RifaManager.Domain.Tests.Tests;

public sealed class RifaTests
{
    [Fact(DisplayName = "Deve criar rifa com dados validos")]
    public void Deve_criar_rifa_com_dados_validos()
    {
        DateOnly dataSorteio = DateOnly.FromDateTime(DateTime.Now.AddDays(10));

        Rifa rifa = new("Rifa teste", "Descricao teste", 10, dataSorteio, "Premio teste");

        rifa.Id.ShouldNotBe(Guid.Empty);
        rifa.Nome.ShouldBe("Rifa teste");
        rifa.Descricao.ShouldBe("Descricao teste");
        rifa.ValorBilhete.ShouldBe(10);
        rifa.DataSorteio.ShouldBe(dataSorteio);
        rifa.Premio.ShouldBe("Premio teste");
        rifa.Encerrada.ShouldBeFalse();
        rifa.Bilhetes.ShouldBeEmpty();
    }

    [Fact(DisplayName = "Deve criar rifa encerrada quando parametro for informado")]
    public void Deve_criar_rifa_encerrada_quando_parametro_for_informado()
    {
        Rifa rifa = new("Rifa teste", "Descricao teste", 10, DateOnly.FromDateTime(DateTime.Now.AddDays(1)), "Premio teste", true);

        rifa.Encerrada.ShouldBeTrue();
    }

    [Fact(DisplayName = "Nao deve criar rifa sem nome")]
    public void Nao_deve_criar_rifa_sem_nome()
    {
        DomainException exception = Should.Throw<DomainException>(() =>
            new Rifa(string.Empty, "Descricao teste", 10, DateOnly.FromDateTime(DateTime.Now.AddDays(1)), "Premio teste"));

        exception.Error.ShouldBe(RifaErrors.NomeObrigatorio);
    }

    [Fact(DisplayName = "Nao deve criar rifa sem descricao")]
    public void Nao_deve_criar_rifa_sem_descricao()
    {
        DomainException exception = Should.Throw<DomainException>(() =>
            new Rifa("Rifa teste", string.Empty, 10, DateOnly.FromDateTime(DateTime.Now.AddDays(1)), "Premio teste"));

        exception.Error.ShouldBe(RifaErrors.DescricaoObrigatoria);
    }

    [Theory(DisplayName = "Nao deve criar rifa com valor do bilhete invalido")]
    [InlineData(0)]
    [InlineData(-1)]
    public void Nao_deve_criar_rifa_com_valor_do_bilhete_invalido(decimal valorBilhete)
    {
        DomainException exception = Should.Throw<DomainException>(() =>
            new Rifa("Rifa teste", "Descricao teste", valorBilhete, DateOnly.FromDateTime(DateTime.Now.AddDays(1)), "Premio teste"));

        exception.Error.ShouldBe(RifaErrors.ValorBilheteInvalido);
    }

    [Fact(DisplayName = "Nao deve criar rifa sem data de sorteio")]
    public void Nao_deve_criar_rifa_sem_data_de_sorteio()
    {
        DomainException exception = Should.Throw<DomainException>(() =>
            new Rifa("Rifa teste", "Descricao teste", 10, default, "Premio teste"));

        exception.Error.ShouldBe(RifaErrors.DataSorteioObrigatoria);
    }

    [Fact(DisplayName = "Nao deve criar rifa com data de sorteio passada")]
    public void Nao_deve_criar_rifa_com_data_de_sorteio_passada()
    {
        DomainException exception = Should.Throw<DomainException>(() =>
            new Rifa("Rifa teste", "Descricao teste", 10, DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), "Premio teste"));

        exception.Error.ShouldBe(RifaErrors.DataSorteioPassada);
    }

    [Fact(DisplayName = "Nao deve criar rifa sem premio")]
    public void Nao_deve_criar_rifa_sem_premio()
    {
        DomainException exception = Should.Throw<DomainException>(() =>
            new Rifa("Rifa teste", "Descricao teste", 10, DateOnly.FromDateTime(DateTime.Now.AddDays(1)), string.Empty));

        exception.Error.ShouldBe(RifaErrors.PremioObrigatorio);
    }

    [Fact(DisplayName = "Deve atualizar rifa com dados validos")]
    public void Deve_atualizar_rifa_com_dados_validos()
    {
        Rifa rifa = CriarRifa();
        DateOnly novaData = DateOnly.FromDateTime(DateTime.Now.AddDays(5));

        rifa.Atualizar("Nova rifa", "Nova descricao", 15, novaData, "Novo premio");

        rifa.Nome.ShouldBe("Nova rifa");
        rifa.Descricao.ShouldBe("Nova descricao");
        rifa.ValorBilhete.ShouldBe(15);
        rifa.DataSorteio.ShouldBe(novaData);
        rifa.Premio.ShouldBe("Novo premio");
    }

    [Fact(DisplayName = "Nao deve atualizar rifa com dados invalidos")]
    public void Nao_deve_atualizar_rifa_com_dados_invalidos()
    {
        Rifa rifa = CriarRifa();

        DomainException exception = Should.Throw<DomainException>(() =>
            rifa.Atualizar(string.Empty, "Descricao teste", 10, DateOnly.FromDateTime(DateTime.Now.AddDays(1)), "Premio teste"));

        exception.Error.ShouldBe(RifaErrors.NomeObrigatorio);
    }

    [Fact(DisplayName = "Deve validar compra de bilhetes em rifa aberta")]
    public void Deve_validar_compra_de_bilhetes_em_rifa_aberta()
    {
        Rifa rifa = CriarRifa();

        Should.NotThrow(rifa.ValidarCompraDeBilhetes);
    }

    [Fact(DisplayName = "Nao deve validar compra de bilhetes em rifa encerrada")]
    public void Nao_deve_validar_compra_de_bilhetes_em_rifa_encerrada()
    {
        Rifa rifa = CriarRifa();
        rifa.Encerrar();

        DomainException exception = Should.Throw<DomainException>(rifa.ValidarCompraDeBilhetes);

        exception.Error.ShouldBe(RifaErrors.CompraEmRifaEncerrada);
    }

    [Fact(DisplayName = "Deve encerrar rifa aberta")]
    public void Deve_encerrar_rifa_aberta()
    {
        Rifa rifa = CriarRifa();

        rifa.Encerrar();

        rifa.Encerrada.ShouldBeTrue();
    }

    [Fact(DisplayName = "Nao deve encerrar rifa ja encerrada")]
    public void Nao_deve_encerrar_rifa_ja_encerrada()
    {
        Rifa rifa = CriarRifa();
        rifa.Encerrar();

        DomainException exception = Should.Throw<DomainException>(rifa.Encerrar);

        exception.Error.ShouldBe(RifaErrors.JaEncerrada);
    }

    [Fact(DisplayName = "Deve marcar bilhete da rifa como pago")]
    public void Deve_marcar_bilhete_da_rifa_como_pago()
    {
        Rifa rifa = CriarRifa();
        Bilhete bilhete = CriarBilhete(rifa);

        rifa.MarcarBilheteComoPago(bilhete);

        bilhete.Status.ShouldBe(StatusPagamento.Pago);
    }

    [Fact(DisplayName = "Deve marcar bilhete da rifa como cancelado")]
    public void Deve_marcar_bilhete_da_rifa_como_cancelado()
    {
        Rifa rifa = CriarRifa();
        Bilhete bilhete = CriarBilhete(rifa);

        rifa.MarcarBilheteComoCancelado(bilhete);

        bilhete.Status.ShouldBe(StatusPagamento.Cancelado);
    }

    [Fact(DisplayName = "Nao deve alterar bilhete quando rifa estiver encerrada")]
    public void Nao_deve_alterar_bilhete_quando_rifa_estiver_encerrada()
    {
        Rifa rifa = CriarRifa();
        Bilhete bilhete = CriarBilhete(rifa);
        rifa.Encerrar();

        DomainException exception = Should.Throw<DomainException>(() =>
            rifa.MarcarBilheteComoPago(bilhete));

        exception.Error.ShouldBe(RifaErrors.BilheteAlteracaoEmRifaEncerrada);
    }

    [Fact(DisplayName = "Nao deve alterar bilhete que nao pertence a rifa")]
    public void Nao_deve_alterar_bilhete_que_nao_pertence_a_rifa()
    {
        Rifa rifa = CriarRifa();
        Bilhete bilhete = CriarBilhete(CriarRifa());

        DomainException exception = Should.Throw<DomainException>(() =>
            rifa.MarcarBilheteComoPago(bilhete));

        exception.Error.ShouldBe(RifaErrors.BilheteNaoPertenceARifa);
    }
}
