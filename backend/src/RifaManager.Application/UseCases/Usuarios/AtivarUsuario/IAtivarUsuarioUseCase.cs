namespace RifaManager.Application.UseCases.Usuarios.AtivarUsuario;

public interface IAtivarUsuarioUseCase
{
    Task Execute(Guid id);
}
