using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Bilhetes.RegistrarCompraBilhetes;

public interface IRegistrarCompraBilhetesUseCase : IUseCase
{
    Task<RegistrarCompraBilhetesResponse> Execute(Guid usuarioResponsavelId, RegistrarCompraBilhetesRequest request, CancellationToken cancellationToken);
}
