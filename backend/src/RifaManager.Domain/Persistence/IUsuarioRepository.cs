using RifaManager.Domain.Entities;

namespace RifaManager.Domain.Persistence;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(Guid id);

    Task<Usuario?> GetByEmailAsync(string email);

    Task AddAsync(Usuario usuario);

    Task UpdateAsync(Usuario usuario);
}
