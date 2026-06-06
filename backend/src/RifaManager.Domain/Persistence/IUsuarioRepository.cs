using RifaManager.Domain.Entities;

namespace RifaManager.Domain.Persistence;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByEmailAsync(string email);
}
