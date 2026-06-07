using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Bilhetes.ListarPorRifa;

public interface IListarBilhetesPorRifaUseCase : IUseCase
{
    Task<IReadOnlyList<ListarBilhetesPorRifaResponse>> Execute(Guid rifaId, CancellationToken cancellationToken);
}
