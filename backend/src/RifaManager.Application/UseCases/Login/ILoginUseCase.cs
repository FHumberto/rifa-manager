namespace RifaManager.Application.UseCases.Login;

public interface ILoginUseCase
{
    Task<LoginResponse> Execute(LoginRequest request, CancellationToken cancellationToken);
}
