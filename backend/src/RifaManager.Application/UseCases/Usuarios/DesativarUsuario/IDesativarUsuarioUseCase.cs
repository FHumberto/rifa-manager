namespace RifaManager.Application.UseCases.DesativarUsuario;

public interface IDesativarUsuarioUseCase
{
    Task Execute(Guid id);
}
