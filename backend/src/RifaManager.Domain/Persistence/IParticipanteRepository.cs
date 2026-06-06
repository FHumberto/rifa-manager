using RifaManager.Domain.Entities;
using RifaManager.Domain.Enums;

namespace RifaManager.Domain.Persistence;

public interface IParticipanteRepository
{
    Task<Participante?> GetByIdAsync(Guid id);

    Task<Participante?> GetByIdWithBilhetesAsync(Guid id);

    Task<IReadOnlyList<Participante>> GetByRifaIdAsync(Guid rifaId);

    Task<IReadOnlyList<Participante>> SearchAsync(string? nome, string? telefone, int? numeroBilhete, StatusPagamento? statusPagamento);

    Task AddAsync(Participante participante);

    Task UpdateAsync(Participante participante);
}
