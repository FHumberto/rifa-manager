namespace RifaManager.Application.UseCases.Bilhetes.ListarPorRifa;

public interface IListarBilhetesPorRifaUseCase
{
    Task<IReadOnlyList<ListarBilhetesPorRifaResponse>> Execute(Guid rifaId);
}
