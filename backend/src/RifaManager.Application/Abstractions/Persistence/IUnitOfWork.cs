namespace RifaManager.Application.Abstractions.Persistence;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
