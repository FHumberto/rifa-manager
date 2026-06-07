using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Participantes.GetById;

public interface IGetParticipanteByIdUseCase : IUseCase
{
    Task<GetParticipanteByIdResponse> Execute(Guid id, CancellationToken cancellationToken);
}
