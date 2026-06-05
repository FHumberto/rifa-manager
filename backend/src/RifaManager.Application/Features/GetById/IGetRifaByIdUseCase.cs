using Ardalis.Result;

namespace RifaManager.Application.Features.GetById;

public interface IGetRifaByIdUseCase
{
    Task<Result<GetRifaByIdResponse>> Execute(Guid id);
}
