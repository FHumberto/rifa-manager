using RifaManager.Application.Exceptions;
using RifaManager.Application.UseCases.DesativarUsuario;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;

namespace RifaManager.Application.UseCases.Usuarios.DesativarUsuario;

public sealed class DesativarUsuarioUseCaseHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork) : IDesativarUsuarioUseCase
{
    public async Task Execute(Guid id)
    {
        Usuario usuario = await usuarioRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(UsuarioErrors.UsuarioNaoEncontrado.Description);

        usuario.Desativar();

        await usuarioRepository.UpdateAsync(usuario);
        await unitOfWork.CommitAsync();
    }
}
