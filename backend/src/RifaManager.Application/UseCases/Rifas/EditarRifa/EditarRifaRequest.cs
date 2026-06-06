namespace RifaManager.Application.UseCases.Rifas.EditarRifa;

public record EditarRifaRequest(string Nome, string Descricao, decimal ValorBilhete, DateOnly DataSorteio, string Premio);
