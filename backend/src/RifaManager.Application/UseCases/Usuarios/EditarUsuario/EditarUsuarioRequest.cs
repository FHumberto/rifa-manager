using RifaManager.Domain.Enums;

namespace RifaManager.Application.UseCases.EditarUsuario;

public record EditarUsuarioRequest(string Nome, string Email, PerfilUsuario Perfil);
