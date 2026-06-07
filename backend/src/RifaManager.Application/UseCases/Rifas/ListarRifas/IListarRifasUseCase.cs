using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Rifas.ListarRifas;

public interface IListarRifasUseCase : IUseCase
{
    Task<IReadOnlyList<ListarRifasResponse>> Execute(CancellationToken cancellationToken);
}
