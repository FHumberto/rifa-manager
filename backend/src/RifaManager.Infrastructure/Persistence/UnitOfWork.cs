using RifaManager.Application.Abstractions.Persistence;
using RifaManager.Infrastructure.Context;

namespace RifaManager.Infrastructure.Persistence;

public sealed class UnitOfWork(RifaDbContext context) : IUnitOfWork
{
    public Task SaveChangesAsync()
    {
        return context.SaveChangesAsync();
    }
}
