namespace RifaManager.Application.UseCases.Bilhetes.ListarPorStatus;

public record ListarBilhetesPorStatusResponse
(
    Guid Id,
    int Numero,
    string Status,
    DateTime CriadoEm,
    DateTime PagoEm,
    DateTime CanceladoEm,
    Guid RifaId,
    Guid ParticipanteId,
    string ParticipanteNome,
    Guid UsuarioResponsavelId,
    string UsuarioResponsavelNome
);
