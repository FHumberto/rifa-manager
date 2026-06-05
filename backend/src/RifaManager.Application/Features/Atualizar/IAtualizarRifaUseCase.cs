using Ardalis.Result;

namespace RifaManager.Application.Features.Atualizar;

public interface IAtualizarRifaUseCase
{
    Task<Result<AtualizarRifaResponse>> Execute(Guid id, AtualizarRifaRequest request);
}
