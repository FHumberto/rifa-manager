using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;
using RifaManager.Domain.Persistence.Repositories;

namespace RifaManager.Application.UseCases.Usuarios.AtivarUsuario;

public sealed class AtivarUsuarioHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork) : IAtivarUsuarioUseCase
{
    public async Task Execute(Guid id, CancellationToken cancellationToken)
    {
        Usuario usuario = await usuarioRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(UsuarioErrors.UsuarioNaoEncontrado.Description);

        usuario.Ativar();

        await usuarioRepository.UpdateAsync(usuario);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
