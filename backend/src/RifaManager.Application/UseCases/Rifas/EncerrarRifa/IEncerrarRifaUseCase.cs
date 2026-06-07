using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Rifas.EncerrarRifa;

public interface IEncerrarRifaUseCase : IUseCase
{
    Task Execute(Guid id, CancellationToken cancellationToken);
}
