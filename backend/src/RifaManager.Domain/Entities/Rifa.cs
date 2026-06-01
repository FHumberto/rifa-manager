using RifaManager.Domain.Abstractions;
using RifaManager.Domain.Errors;

namespace RifaManager.Domain.Entities;

public sealed class Rifa : Entity
{
    #region [ PROPRIEDADES ]

    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public decimal ValorBilhete { get; private set; }
    public DateOnly DataSorteio { get; private set; }
    public string Premio { get; private set; }
    public bool Encerrada { get; private set; } = false;

    public List<Bilhete> Bilhetes { get; private set; } = [];

    #endregion

    #region [ CONSTRUTORES ]

    private Rifa()
    {
        Nome = string.Empty;
        Descricao = string.Empty;
        Premio = string.Empty;
    }

    public Rifa(string nome, string descricao, decimal valorBilhete, DateOnly dataSorteio, string premio, bool encerrada = false)
    {
        Nome = nome;
        Descricao = descricao;
        ValorBilhete = valorBilhete;
        DataSorteio = dataSorteio;
        Premio = premio;
        Encerrada = encerrada;

        IsValid();
    }

    #endregion

    #region [ VALIDACOES ]

    public override void IsValid()
    {
        if (string.IsNullOrEmpty(Nome))
            throw new DomainException(RifaErrors.NomeObrigatorio);

        if (string.IsNullOrEmpty(Descricao))
            throw new DomainException(RifaErrors.DescricaoObrigatoria);

        if (ValorBilhete <= 0)
            throw new DomainException(RifaErrors.ValorBilheteInvalido);

        if (DataSorteio == default)
            throw new DomainException(RifaErrors.DataSorteioObrigatoria);

        if (DataSorteio < DateOnly.FromDateTime(DateTime.Now))
            throw new DomainException(RifaErrors.DataSorteioPassada);

        if (string.IsNullOrEmpty(Premio))
            throw new DomainException(RifaErrors.PremioObrigatorio);
    }

    private void ValidarAlteracaoDeBilhete(Bilhete bilhete)
    {
        if (Encerrada)
            throw new DomainException(RifaErrors.BilheteAlteracaoEmRifaEncerrada);

        if (!Bilhetes.Contains(bilhete))
            throw new DomainException(RifaErrors.BilheteNaoPertenceARifa);
    }

    #endregion

    #region [ COMORTAMENTO ]

    public void Atualizar(string nome, string descricao, decimal valorBilhete, DateOnly dataSorteio, string premio)
    {
        Nome = nome;
        Descricao = descricao;
        ValorBilhete = valorBilhete;
        DataSorteio = dataSorteio;
        Premio = premio;

        IsValid();
    }

    public void Encerrar()
    {
        if (Encerrada)
            throw new DomainException(RifaErrors.JaEncerrada);

        Encerrada = true;
    }

    public void MarcarBilheteComoPago(Bilhete bilhete)
    {
        ValidarAlteracaoDeBilhete(bilhete);

        bilhete.MarcarComoPago();
    }

    public void MarcarBilheteComoCancelado(Bilhete bilhete)
    {
        ValidarAlteracaoDeBilhete(bilhete);

        bilhete.MarcarComoCancelado();
    }

    #endregion
}
