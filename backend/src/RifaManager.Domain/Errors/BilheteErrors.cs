using RifaManager.Domain.Abstractions.Types;

namespace RifaManager.Domain.Errors;

public static class BilheteErrors
{
    public static readonly Error NumeroInvalido =
        Error.Validation("Bilhete.NumeroInvalido", "O número do bilhete deve ser maior que zero.");

    public static readonly Error ParticipanteObrigatorio =
        Error.Validation("Bilhete.ParticipanteObrigatorio", "O bilhete deve estar associado a um participante.");

    public static readonly Error RifaObrigatoria =
        Error.Validation("Bilhete.RifaObrigatoria", "O bilhete deve estar associado a uma rifa.");

    public static readonly Error UsuarioResponsavelObrigatorio =
        Error.Validation("Bilhete.UsuarioResponsavelObrigatorio", "O bilhete deve estar associado a um usuário responsável.");

    public static readonly Error PagoECancelado =
        Error.Conflict("Bilhete.PagoECancelado", "Um bilhete não pode ser marcado como pago e cancelado ao mesmo tempo.");

    public static readonly Error CanceladoNaoPodeSerPago =
        Error.Conflict("Bilhete.CanceladoNaoPodeSerPago", "Não é possível marcar um bilhete cancelado como pago.");

    public static readonly Error PagoNaoPodeSerCancelado =
        Error.Conflict("Bilhete.PagoNaoPodeSerCancelado", "Não é possível marcar um bilhete pago como cancelado.");

    public static readonly Error BilheteNaoEncontrado =
        Error.NotFound("Bilhete.BilheteNaoEncontrado", "O bilhete não foi encontrado.");
}
