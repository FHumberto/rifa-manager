namespace RifaManager.Application.UseCases.Participantes.CadastrarParticipante;

public interface ICadastrarParticipanteUseCase
{
    Task<CadastrarParticipanteResponse> Execute(CadastrarParticipanteRequest request, CancellationToken cancellationToken);
}
