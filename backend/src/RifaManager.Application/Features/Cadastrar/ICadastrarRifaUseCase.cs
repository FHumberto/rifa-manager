using Ardalis.Result;

namespace RifaManager.Application.Features.Cadastrar;

public interface ICadastrarRifaUseCase
{
    Task<Result<CadastrarRifaResponse>> Execute(CadastrarRifaRequest request);
}
