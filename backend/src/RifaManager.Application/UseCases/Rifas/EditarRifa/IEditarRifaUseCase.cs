using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Rifas.EditarRifa;

public interface IEditarRifaUseCase : IUseCase
{
    Task Execute(Guid id, EditarRifaRequest request, CancellationToken cancellationToken);
}
