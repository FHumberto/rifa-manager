using RifaManager.Domain.Abstractions;

namespace RifaManager.Domain.Errors;

public static class ParticipanteErrors
{
    public static readonly Error NomeObrigatorio =
        Error.Validation("Participante.NomeObrigatorio", "O nome do participante é obrigatório");

    public static readonly Error TelefoneObrigatorio =
        Error.Validation("Participante.TelefoneObrigatorio", "O telefone do participante é obrigatório");

    public static readonly Error ParticipanteNaoEncontrado =
        Error.NotFound("Participante.ParticipanteNaoEncontrado", "Participante não encontrado");
}
