using RifaManager.Domain.Abstractions;

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
            throw new ArgumentException("O nome da rifa é obrigatório.");

        if (string.IsNullOrEmpty(Descricao))
            throw new ArgumentException("A descrição da rifa é obrigatória.");

        if (ValorBilhete <= 0)
            throw new ArgumentException("O valor do bilhete deve ser maior que zero.");

        if (DataSorteio == default)
            throw new ArgumentException("A data do sorteio é obrigatória.");

        if (DataSorteio < DateOnly.FromDateTime(DateTime.Now))
            throw new ArgumentException("A data do sorteio deve ser no futuro.");

        if (string.IsNullOrEmpty(Premio))
            throw new ArgumentException("O prêmio da rifa é obrigatório.");
    }

    private void ValidarAlteracaoDeBilhete(Bilhete bilhete)
    {
        if (Encerrada)
            throw new InvalidOperationException("Não é possível alterar bilhetes de uma rifa encerrada.");

        if (!Bilhetes.Contains(bilhete))
            throw new ArgumentException("O bilhete informado não pertence a esta rifa.");
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
            throw new InvalidOperationException("A rifa já está encerrada.");

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
