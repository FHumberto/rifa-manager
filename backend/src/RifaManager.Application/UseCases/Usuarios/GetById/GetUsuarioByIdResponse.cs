namespace RifaManager.Application.UseCases.Usuarios.GetById;

public record GetUsuarioByIdResponse(Guid Id, string Nome, string Email, string Perfil, bool Ativo);
