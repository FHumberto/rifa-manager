namespace RifaManager.Application.UseCases.Rifas.GetById;

public record GetRifaByIdResponse(Guid Id, string Nome, string Descricao, decimal ValorBilhete, DateOnly DataSorteio, string Premio, bool Encerrada);
