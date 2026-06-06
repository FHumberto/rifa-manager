using RifaManager.Domain.Enums;

namespace RifaManager.Application.UseCases.Participantes.PesquisarParticipantes;

public record PesquisarParticipantesRequest(string? Nome, string? Telefone, int? NumeroBilhete, StatusPagamento? StatusPagamento);
