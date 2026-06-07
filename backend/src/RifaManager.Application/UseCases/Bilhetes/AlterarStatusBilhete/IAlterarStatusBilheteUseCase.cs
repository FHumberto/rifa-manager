using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Bilhetes.AlterarStatusBilhete;

public interface IAlterarStatusBilheteUseCase : IUseCase
{
    Task Execute(Guid id, AlterarStatusBilheteRequest request, CancellationToken cancellationToken);
}
