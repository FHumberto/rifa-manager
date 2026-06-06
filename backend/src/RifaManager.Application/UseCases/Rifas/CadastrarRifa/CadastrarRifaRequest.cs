namespace RifaManager.Application.UseCases.Rifas.CadastrarRifa;

public record CadastrarRifaRequest(string Nome, string Descricao, decimal ValorBilhete, DateOnly DataSorteio, string Premio);
