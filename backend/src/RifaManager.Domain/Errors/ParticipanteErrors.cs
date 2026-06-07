using RifaManager.Domain.Abstractions.Types;

namespace RifaManager.Domain.Errors;

public static class ParticipanteErrors
{
    public static readonly Error ParticipanteObrigatorio =
        Error.Validation("Participante.ParticipanteObrigatorio", "O participante é obrigatório");

    public static readonly Error NomeObrigatorio =
        Error.Validation("Participante.NomeObrigatorio", "O nome do participante é obrigatório");

    public static readonly Error NomeMuitoLongo =
        Error.Validation("Participante.NomeMuitoLongo", "O nome do participante deve conter no máximo 100 caracteres");

    public static readonly Error TelefoneObrigatorio =
        Error.Validation("Participante.TelefoneObrigatorio", "O telefone do participante é obrigatório");

    public static readonly Error TelefoneMuitoLongo =
        Error.Validation("Participante.TelefoneMuitoLongo", "O telefone do participante deve conter no máximo 20 caracteres");

    public static readonly Error ParticipanteNaoEncontrado =
        Error.NotFound("Participante.ParticipanteNaoEncontrado", "Participante não encontrado");

    public static readonly Error ParticipanteVinculadoRifaEncerrada =
        Error.Validation("Participante.ParticipanteVinculadoRifaEncerrada", "Não é possível editar participante vinculado a uma rifa encerrada.");
}
