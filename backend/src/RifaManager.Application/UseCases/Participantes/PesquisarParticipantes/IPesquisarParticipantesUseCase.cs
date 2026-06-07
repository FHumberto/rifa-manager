using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Participantes.PesquisarParticipantes;

public interface IPesquisarParticipantesUseCase : IUseCase
{
    Task<IReadOnlyList<PesquisarParticipantesResponse>> Execute(PesquisarParticipantesRequest request, CancellationToken cancellationToken);
}
