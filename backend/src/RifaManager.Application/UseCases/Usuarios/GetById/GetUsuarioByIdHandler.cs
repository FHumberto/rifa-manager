using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;

namespace RifaManager.Application.UseCases.Usuarios.GetById;

public sealed class GetUsuarioByIdHandler(IUsuarioRepository usuarioRepository) : IGetUsuarioByIdUseCase
{
    public async Task<GetUsuarioByIdResponse> Execute(Guid id)
    {
        Usuario usuario = await usuarioRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(UsuarioErrors.UsuarioNaoEncontrado.Description);

        return new GetUsuarioByIdResponse
        (
            usuario.Id,
            usuario.Nome,
            usuario.Email,
            usuario.Perfil.ToString(),
            usuario.Ativo
        );
    }
}
