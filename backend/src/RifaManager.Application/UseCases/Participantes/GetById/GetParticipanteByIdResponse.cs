namespace RifaManager.Application.UseCases.Participantes.GetById;

public record GetParticipanteByIdResponse(Guid Id, string Nome, string Telefone, string? Observacao, IReadOnlyList<ParticipanteBilheteResponse> Bilhetes);

public record ParticipanteBilheteResponse(Guid Id, int Numero, Guid RifaId, string Status);
