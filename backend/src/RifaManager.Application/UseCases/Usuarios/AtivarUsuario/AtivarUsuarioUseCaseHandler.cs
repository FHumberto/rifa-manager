using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;

namespace RifaManager.Application.UseCases.Usuarios.AtivarUsuario;

public sealed class AtivarUsuarioUseCaseHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork) : IAtivarUsuarioUseCase
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
