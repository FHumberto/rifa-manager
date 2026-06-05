namespace RifaManager.Application.Features.Listar;

public record ListarRifasResponse(Guid Id, string Nome, decimal ValorBilhete, int QuantidadeDeBilhetes, DateOnly DataSorteio, bool Encerrada);
