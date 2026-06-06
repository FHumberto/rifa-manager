namespace RifaManager.Application.UseCases.Bilhetes.CancelarBilhete;

public interface ICancelarBilheteUseCase
{
    Task Execute(Guid id);
}
