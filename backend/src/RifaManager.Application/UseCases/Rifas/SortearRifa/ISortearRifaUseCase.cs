using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Rifas.SortearRifa;

public interface ISortearRifaUseCase : IUseCase
{
    Task<SortearRifaResponse> Execute(Guid id, CancellationToken cancellationToken);
}
