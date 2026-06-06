namespace RifaManager.Application.UseCases.Rifas.EditarRifa;

public interface IEditarRifaUseCase
{
    Task Execute(Guid id, EditarRifaRequest request);
}
