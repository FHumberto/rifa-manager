using Microsoft.EntityFrameworkCore;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Persistence;
using RifaManager.Infrastructure.Context;

namespace RifaManager.Infrastructure.Persistence;

internal sealed class UsuarioRepository(RifaDbContext context) : IUsuarioRepository
{
    public Task<Usuario?> GetByEmailAsync(string email)
    {
        return context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(usuario => usuario.Email == email);
    }
}
