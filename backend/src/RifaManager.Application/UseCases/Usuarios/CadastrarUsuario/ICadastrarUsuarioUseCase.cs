using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Usuarios.CadastrarUsuario;

public interface ICadastrarUsuarioUseCase : IUseCase
{
    Task<CadastrarUsuarioResponse> Execute(CadastrarUsuarioRequest request, CancellationToken cancellationToken);
}
