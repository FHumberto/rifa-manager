namespace RifaManager.Application.UseCases.Participantes.GetById;

public interface IGetParticipanteByIdUseCase
{
    Task<GetParticipanteByIdResponse> Execute(Guid id);
}
