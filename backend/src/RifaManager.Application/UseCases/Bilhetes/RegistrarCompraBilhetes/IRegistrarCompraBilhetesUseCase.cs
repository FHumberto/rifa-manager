namespace RifaManager.Application.UseCases.Bilhetes.RegistrarCompraBilhetes;

public interface IRegistrarCompraBilhetesUseCase
{
    Task<RegistrarCompraBilhetesResponse> Execute(Guid usuarioResponsavelId, RegistrarCompraBilhetesRequest request);
}
