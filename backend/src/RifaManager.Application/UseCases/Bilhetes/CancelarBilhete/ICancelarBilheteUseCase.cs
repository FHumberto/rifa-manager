using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Bilhetes.CancelarBilhete;

public interface ICancelarBilheteUseCase : IUseCase
{
    Task Execute(Guid id, CancellationToken cancellationToken);
}
