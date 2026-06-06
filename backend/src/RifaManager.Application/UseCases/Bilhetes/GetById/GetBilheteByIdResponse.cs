namespace RifaManager.Application.UseCases.Bilhetes.GetById;

public record GetBilheteByIdResponse
(
    Guid Id,
    int Numero,
    string Status,
    DateTime CriadoEm,
    DateTime PagoEm,
    DateTime CanceladoEm,
    Guid RifaId,
    Guid ParticipanteId,
    string ParticipanteNome,
    string ParticipanteTelefone,
    Guid UsuarioResponsavelId,
    string UsuarioResponsavelNome
);
