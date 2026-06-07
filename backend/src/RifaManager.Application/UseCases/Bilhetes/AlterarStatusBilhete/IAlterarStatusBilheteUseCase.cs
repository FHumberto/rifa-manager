namespace RifaManager.Application.UseCases.Bilhetes.AlterarStatusBilhete;

public interface IAlterarStatusBilheteUseCase
{
    Task Execute(Guid id, AlterarStatusBilheteRequest request, CancellationToken cancellationToken);
}
