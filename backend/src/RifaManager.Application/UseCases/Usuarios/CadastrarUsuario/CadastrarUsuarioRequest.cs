using RifaManager.Domain.Enums;

namespace RifaManager.Application.UseCases.Usuarios.CadastrarUsuario;

public record CadastrarUsuarioRequest(string Nome, string Email, string Senha, PerfilUsuario Perfil);
