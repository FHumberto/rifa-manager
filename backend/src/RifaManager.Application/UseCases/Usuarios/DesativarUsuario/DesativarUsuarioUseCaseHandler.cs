using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;
using RifaManager.Domain.Persistence.Repositories;

namespace RifaManager.Application.UseCases.Usuarios.DesativarUsuario;

public sealed class DesativarUsuarioUseCaseHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork) : IDesativarUsuarioUseCase
{
    public async Task Execute(Guid id, CancellationToken cancellationToken)
    {
        Usuario usuario = await usuarioRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(UsuarioErrors.UsuarioNaoEncontrado.Description);

        usuario.Desativar();

        await usuarioRepository.UpdateAsync(usuario);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
