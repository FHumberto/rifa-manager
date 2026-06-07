using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Participantes.CadastrarParticipante;

public interface ICadastrarParticipanteUseCase : IUseCase
{
    Task<CadastrarParticipanteResponse> Execute(CadastrarParticipanteRequest request, CancellationToken cancellationToken);
}
