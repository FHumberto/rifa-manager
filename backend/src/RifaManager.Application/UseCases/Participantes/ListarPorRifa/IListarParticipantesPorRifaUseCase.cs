using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Participantes.ListarPorRifa;

public interface IListarParticipantesPorRifaUseCase : IUseCase
{
    Task<IReadOnlyList<ListarParticipantesPorRifaResponse>> Execute(Guid rifaId, CancellationToken cancellationToken);
}
