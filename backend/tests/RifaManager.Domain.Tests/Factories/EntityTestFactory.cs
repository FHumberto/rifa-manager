using RifaManager.Domain.Entities;
using RifaManager.Domain.Enums;

namespace RifaManager.Domain.Tests.Factories;

internal static class EntityTestFactory
{
    public static Rifa CriarRifa()
        => new("Rifa teste", "Descricao teste", 10, DateOnly.FromDateTime(DateTime.Now.AddDays(1)), "Premio teste");

    public static Bilhete CriarBilhete(Rifa rifa)
        => new(1, rifa, CriarParticipante(), CriarAdministrador());

    public static Participante CriarParticipante()
        => new("Joao", "84999999999", null);

    public static Usuario CriarAdministrador()
        => new("Admin", "admin@rifa.com", PerfilUsuario.Administrador, true);

    public static Usuario CriarUsuarioComum(bool ativo = true)
        => new("Usuario", "usuario@rifa.com", PerfilUsuario.Comum, ativo);
}
