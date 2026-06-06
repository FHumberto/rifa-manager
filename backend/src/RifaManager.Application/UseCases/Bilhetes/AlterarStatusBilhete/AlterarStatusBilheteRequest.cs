using RifaManager.Domain.Enums;

namespace RifaManager.Application.UseCases.Bilhetes.AlterarStatusBilhete;

public record AlterarStatusBilheteRequest(StatusPagamento Status);
