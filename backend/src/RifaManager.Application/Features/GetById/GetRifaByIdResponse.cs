namespace RifaManager.Application.Features.GetById;

public record GetRifaByIdResponse(string Nome, decimal ValorBilhete, int quantidadeDeBilhetes, DateOnly DataSorteio);
