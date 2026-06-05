using Ardalis.Result;

namespace RifaManager.Application.Features.Encerrar;

public interface IEncerrarRifaUseCase
{
    Task<Result<EncerrarRifaResponse>> Execute(Guid id);
}
