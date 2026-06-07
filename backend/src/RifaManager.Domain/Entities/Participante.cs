using RifaManager.Domain.Abstractions;
using RifaManager.Domain.Abstractions.Types;
using RifaManager.Domain.Errors;

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
            throw new DomainException(ParticipanteErrors.NomeObrigatorio);

        if (string.IsNullOrEmpty(Telefone))
            throw new DomainException(ParticipanteErrors.TelefoneObrigatorio);
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
