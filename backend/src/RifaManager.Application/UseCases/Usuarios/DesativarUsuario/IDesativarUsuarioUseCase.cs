using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Usuarios.DesativarUsuario;

public interface IDesativarUsuarioUseCase : IUseCase
{
    Task Execute(Guid id, CancellationToken cancellationToken);
}
