namespace RifaManager.Application.UseCases.Bilhetes.GetById;

public interface IGetBilheteByIdUseCase
{
    Task<GetBilheteByIdResponse> Execute(Guid id, CancellationToken cancellationToken);
}
