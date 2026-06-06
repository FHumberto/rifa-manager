using RifaManager.Domain.Entities;

namespace RifaManager.Domain.Persistence;

public interface IParticipanteRepository
{
    Task<Participante?> GetByIdAsync(Guid id);
}
