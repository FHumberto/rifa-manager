using RifaManager.Domain.Abstractions;

namespace RifaManager.Domain.Entities;

public sealed class Participante : Entity
{
    #region [ PROPRIEDADES ]

    public string Nome { get; private set; }
    public string Telefone { get; private set; }
    public string? Observacao { get; private set; }

    public List<Bilhete> Bilhetes { get; private set; } = [];

    #endregion

    #region [ CONSTRUTORES ]

    private Participante()
    {
        Nome = string.Empty;
        Telefone = string.Empty;
    }

    public Participante(string nome, string telefone, string? observacao)
    {
        Nome = nome;
        Telefone = telefone;
        Observacao = observacao;

        IsValid();
    }

    #endregion

    #region [ VALIDACOES ]

    public override void IsValid()
    {
        if (string.IsNullOrEmpty(Nome))
            throw new ArgumentException("O nome do participante é obrigatório");

        if (string.IsNullOrEmpty(Telefone))
            throw new ArgumentException("O telefone do participante é obrigatório");
    }

    #endregion

    #region [ COMORTAMENTO ]

    public void Atualizar(string nome, string telefone, string? observacao)
    {
        Nome = nome;
        Telefone = telefone;
        Observacao = observacao;

        IsValid();
    }

    #endregion
}
