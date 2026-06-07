using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Usuarios.AtivarUsuario;

public interface IAtivarUsuarioUseCase : IUseCase
{
    Task Execute(Guid id, CancellationToken cancellationToken);
}
