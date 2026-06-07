using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Login;

public interface ILoginUseCase : IUseCase
{
    Task<LoginResponse> Execute(LoginRequest request, CancellationToken cancellationToken);
}
