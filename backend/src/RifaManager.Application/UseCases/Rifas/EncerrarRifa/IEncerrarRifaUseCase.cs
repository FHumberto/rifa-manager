namespace RifaManager.Application.UseCases.Rifas.EncerrarRifa;

public interface IEncerrarRifaUseCase
{
    Task Execute(Guid id);
}
