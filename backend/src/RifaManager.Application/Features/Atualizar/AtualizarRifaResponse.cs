namespace RifaManager.Application.Features.Atualizar;

public record AtualizarRifaResponse(Guid Id, string Nome, decimal ValorBilhete, DateOnly DataSorteio, string Premio);
