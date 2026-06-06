namespace RifaManager.Application.UseCases.Usuarios.EditarUsuario;

public interface IEditarUsuarioUseCase
{
    Task Execute(Guid id, EditarUsuarioRequest request);
}
