namespace RifaManager.Application.UseCases.Rifas.SortearRifa;

public interface ISortearRifaUseCase
{
    Task<SortearRifaResponse> Execute(Guid id);
}
