namespace RifaManager.Application.UseCases.Usuarios.GetById;

public interface IGetUsuarioByIdUseCase
{
    Task<GetUsuarioByIdResponse> Execute(Guid id);
}
