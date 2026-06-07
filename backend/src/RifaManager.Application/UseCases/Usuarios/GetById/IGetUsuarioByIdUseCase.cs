using RifaManager.Application.Abstractions.Markers;

namespace RifaManager.Application.UseCases.Usuarios.GetById;

public interface IGetUsuarioByIdUseCase : IUseCase
{
    Task<GetUsuarioByIdResponse> Execute(Guid id, CancellationToken cancellationToken);
}
