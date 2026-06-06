namespace RifaManager.Application.UseCases.Rifas.ListarRifas;

public record ListarRifasResponse(Guid Id, string Nome, string Descricao, decimal ValorBilhete, DateOnly DataSorteio, string Premio, bool Encerrada);
