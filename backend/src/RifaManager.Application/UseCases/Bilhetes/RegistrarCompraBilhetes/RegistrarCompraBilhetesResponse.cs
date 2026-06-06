namespace RifaManager.Application.UseCases.Bilhetes.RegistrarCompraBilhetes;

public record RegistrarCompraBilhetesResponse(Guid RifaId, Guid ParticipanteId, IReadOnlyList<BilheteRegistradoResponse> Bilhetes);

public record BilheteRegistradoResponse(Guid Id, int Numero);
