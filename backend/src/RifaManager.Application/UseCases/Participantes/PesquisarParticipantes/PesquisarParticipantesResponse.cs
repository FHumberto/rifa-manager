namespace RifaManager.Application.UseCases.Participantes.PesquisarParticipantes;

public record PesquisarParticipantesResponse(Guid Id, string Nome, string Telefone, string? Observacao);
