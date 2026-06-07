using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Rifas.CadastrarRifa;

public interface ICadastrarRifaUseCase : IUseCase
{
    Task<CadastrarRifaResponse> Execute(CadastrarRifaRequest request, CancellationToken cancellationToken);
}
