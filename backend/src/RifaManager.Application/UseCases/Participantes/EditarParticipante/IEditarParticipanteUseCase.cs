using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Participantes.EditarParticipante;

public interface IEditarParticipanteUseCase : IUseCase
{
    Task Execute(Guid id, EditarParticipanteRequest request, CancellationToken cancellationToken);
}
