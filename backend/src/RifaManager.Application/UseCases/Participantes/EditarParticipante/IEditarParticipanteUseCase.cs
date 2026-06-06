namespace RifaManager.Application.UseCases.Participantes.EditarParticipante;

public interface IEditarParticipanteUseCase
{
    Task Execute(Guid id, EditarParticipanteRequest request);
}
