namespace RifaManager.Application.UseCases.Bilhetes.RegistrarCompraBilhetes;

public record RegistrarCompraBilhetesRequest(Guid RifaId, Guid ParticipanteId, int Quantidade);
