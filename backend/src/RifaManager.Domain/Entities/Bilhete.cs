using RifaManager.Domain.Abstractions;
using RifaManager.Domain.Enums;

namespace RifaManager.Domain.Entities;

public sealed class Bilhete : Entity
{
    #region [ PROPRIEDADES ]

    public int Numero { get; private set; }
    public StatusPagamento Status { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public DateTime PagoEm { get; private set; }
    public DateTime CanceladoEm { get; private set; }

    public Guid RifaId { get; private set; }
    public Rifa Rifa { get; private set; }

    public Guid ParticipanteId { get; private set; }
    public Participante Participante { get; private set; }

    public Guid UsuarioResponsavelId { get; private set; }
    public Usuario UsuarioResponsavel { get; private set; }

    #endregion

    #region [ CONSTRUTORES ]

    private Bilhete()
    {
        Rifa = null!;
        Participante = null!;
        UsuarioResponsavel = null!;
    }

    public Bilhete(int numero, Rifa rifa, Participante participante, Usuario usuarioResponsavel)
    {
        ArgumentNullException.ThrowIfNull(rifa);
        ArgumentNullException.ThrowIfNull(participante);
        ArgumentNullException.ThrowIfNull(usuarioResponsavel);

        Numero = numero;
        Rifa = rifa;
        RifaId = rifa.Id;
        Participante = participante;
        ParticipanteId = participante.Id;
        UsuarioResponsavel = usuarioResponsavel;
        UsuarioResponsavelId = usuarioResponsavel.Id;
        Status = StatusPagamento.Pendente;
        CriadoEm = DateTime.UtcNow;

        IsValid();

        Rifa.Bilhetes.Add(this);
        Participante.Bilhetes.Add(this);
        UsuarioResponsavel.BilhetesVendidos.Add(this);
    }

    #endregion

    #region [ VALIDACOES ]

    public override void IsValid()
    {
        if (Numero <= 0)
            throw new ArgumentException("O número do bilhete deve ser maior que zero.");

        if (Participante is null)
            throw new ArgumentException("O bilhete deve estar associado a um participante.");

        if (Rifa is null)
            throw new ArgumentException("O bilhete deve estar associado a uma rifa.");

        if (UsuarioResponsavel is null)
            throw new ArgumentException("O bilhete deve estar associado a um usuário responsável.");

        if (PagoEm != default && CanceladoEm != default)
            throw new ArgumentException("Um bilhete não pode ser marcado como pago e cancelado ao mesmo tempo.");
    }

    #endregion

    #region [ COMORTAMENTO ]

    internal void MarcarComoPago()
    {
        if (Status.Equals(StatusPagamento.Cancelado))
            throw new InvalidOperationException("Não é possível marcar um bilhete cancelado como pago.");

        Status = StatusPagamento.Pago;
        PagoEm = DateTime.UtcNow;
    }

    internal void MarcarComoCancelado()
    {
        if (Status.Equals(StatusPagamento.Pago))
            throw new ArgumentException("Não é possível marcar um bilhete pago como cancelado.");

        Status = StatusPagamento.Cancelado;
        CanceladoEm = DateTime.UtcNow;
    }

    #endregion
}
