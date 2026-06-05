namespace RifaManager.Application.Features.Cadastrar;

public record CadastrarRifaRequest(string Nome, string Descricao, decimal ValorBilhete, DateOnly DataSorteio, string Premio);
