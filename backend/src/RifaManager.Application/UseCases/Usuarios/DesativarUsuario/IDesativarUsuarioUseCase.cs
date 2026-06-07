namespace RifaManager.Application.UseCases.Usuarios.DesativarUsuario;

public interface IDesativarUsuarioUseCase
{
    Task Execute(Guid id, CancellationToken cancellationToken);
}
