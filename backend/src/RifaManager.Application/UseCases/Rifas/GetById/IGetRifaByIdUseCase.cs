using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Rifas.GetById;

public interface IGetRifaByIdUseCase : IUseCase
{
    Task<GetRifaByIdResponse> Execute(Guid id, CancellationToken cancellationToken);
}
