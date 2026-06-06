namespace RifaManager.Application.UseCases.Rifas.ListarRifas;

public interface IListarRifasUseCase
{
    Task<IReadOnlyList<ListarRifasResponse>> Execute();
}
