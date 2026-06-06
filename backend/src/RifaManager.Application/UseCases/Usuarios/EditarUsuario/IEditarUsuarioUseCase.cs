namespace RifaManager.Application.UseCases.EditarUsuario;

public interface IEditarUsuarioUseCase
{
    Task Execute(Guid id, EditarUsuarioRequest request);
}
