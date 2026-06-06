namespace RifaManager.Application.UseCases.Rifas.SortearRifa;

public record SortearRifaResponse
(
    Guid RifaId,
    Guid BilheteId,
    int NumeroBilhete,
    Guid ParticipanteId,
    string ParticipanteNome,
    string ParticipanteTelefone
);
