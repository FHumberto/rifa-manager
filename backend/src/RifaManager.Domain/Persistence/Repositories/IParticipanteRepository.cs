using RifaManager.Domain.Entities;
using RifaManager.Domain.Enums;

namespace RifaManager.Domain.Persistence.Repositories;

public interface IParticipanteRepository
{
    Task<Participante?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Participante?> GetByIdWithBilhetesAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Participante>> GetByRifaIdAsync(Guid rifaId, CancellationToken cancellationToken);

    Task<IReadOnlyList<Participante>> SearchAsync(string? nome, string? telefone, int? numeroBilhete, StatusPagamento? statusPagamento, CancellationToken cancellationToken);

    Task AddAsync(Participante participante, CancellationToken cancellationToken);

    Task UpdateAsync(Participante participante);
}
