namespace RifaManager.Application.UseCases.Participantes.PesquisarParticipantes;

public interface IPesquisarParticipantesUseCase
{
    Task<IReadOnlyList<PesquisarParticipantesResponse>> Execute(PesquisarParticipantesRequest request, CancellationToken cancellationToken);
}
