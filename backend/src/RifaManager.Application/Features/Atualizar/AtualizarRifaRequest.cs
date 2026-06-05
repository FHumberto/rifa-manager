namespace RifaManager.Application.Features.Atualizar;

public record AtualizarRifaRequest(string Nome, string Descricao, decimal ValorBilhete, DateOnly DataSorteio, string Premio);
