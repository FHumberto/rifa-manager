using RifaManager.Domain.Enums;

namespace RifaManager.Application.UseCases.Usuarios.EditarUsuario;

public record EditarUsuarioRequest(string Nome, string Email, PerfilUsuario Perfil);
