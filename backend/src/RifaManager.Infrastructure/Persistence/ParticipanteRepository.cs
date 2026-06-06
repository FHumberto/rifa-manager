using Microsoft.EntityFrameworkCore;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Persistence;
using RifaManager.Infrastructure.Context;

namespace RifaManager.Infrastructure.Persistence;

internal sealed class ParticipanteRepository(RifaDbContext context) : IParticipanteRepository
{
    public Task<Participante?> GetByIdAsync(Guid id)
    {
        return context.Participantes.FirstOrDefaultAsync(participante => participante.Id == id);
    }
}
