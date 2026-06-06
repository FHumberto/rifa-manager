namespace RifaManager.Application.UseCases.Bilhetes.ListarPorRifa;

public record ListarBilhetesPorRifaResponse
(
    Guid Id,
    int Numero,
    string Status,
    DateTime CriadoEm,
    DateTime PagoEm,
    DateTime CanceladoEm,
    Guid ParticipanteId,
    string ParticipanteNome,
    Guid UsuarioResponsavelId,
    string UsuarioResponsavelNome
);
