namespace RifaManager.Application.UseCases.Usuarios.CadastrarUsuario;

public interface ICadastrarUsuarioUseCase
{
    Task<CadastrarUsuarioResponse> Execute(CadastrarUsuarioRequest request, CancellationToken cancellationToken);
}
