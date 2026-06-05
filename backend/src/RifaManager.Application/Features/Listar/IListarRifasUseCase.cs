using Ardalis.Result;

namespace RifaManager.Application.Features.Listar;

public interface IListarRifasUseCase
{
    Task<Result<IReadOnlyList<ListarRifasResponse>>> Execute();
}
