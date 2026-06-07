namespace RifaManager.Application.UseCases.Rifas.GetById;

public interface IGetRifaByIdUseCase
{
    Task<GetRifaByIdResponse> Execute(Guid id, CancellationToken cancellationToken);
}
