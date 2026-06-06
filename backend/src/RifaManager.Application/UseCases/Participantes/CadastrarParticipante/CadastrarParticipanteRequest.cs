namespace RifaManager.Application.UseCases.Participantes.CadastrarParticipante;

public record CadastrarParticipanteRequest(Guid RifaId, string Nome, string Telefone, string? Observacao);
