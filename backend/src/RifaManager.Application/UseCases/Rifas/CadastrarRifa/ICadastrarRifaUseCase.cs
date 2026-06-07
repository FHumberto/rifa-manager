namespace RifaManager.Application.UseCases.Rifas.CadastrarRifa;

public interface ICadastrarRifaUseCase
{
    Task<CadastrarRifaResponse> Execute(CadastrarRifaRequest request, CancellationToken cancellationToken);
}
