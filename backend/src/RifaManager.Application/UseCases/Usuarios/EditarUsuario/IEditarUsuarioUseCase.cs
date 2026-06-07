using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Usuarios.EditarUsuario;

public interface IEditarUsuarioUseCase : IUseCase
{
    Task Execute(Guid id, EditarUsuarioRequest request, CancellationToken cancellationToken);
}
