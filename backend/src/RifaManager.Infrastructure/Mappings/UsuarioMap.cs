using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RifaManager.Domain.Entities;
using RifaManager.Infrastructure.Seeds;

namespace RifaManager.Infrastructure.Mappings;

public sealed class UsuarioMap : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        ConfigureDataStructure(builder);
        ConfigureRelationships(builder);
        ConfigureIndexes(builder);
        ConfigureSeeds(builder);
    }

    private static void ConfigureDataStructure(EntityTypeBuilder<Usuario> builder)
    {
        builder.Property(Usuario => Usuario.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(Usuario => Usuario.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(Usuario => Usuario.Senha)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(Usuario => Usuario.Perfil)
            .IsRequired();

        builder.Property(Usuario => Usuario.Ativo)
            .IsRequired();
    }

    private static void ConfigureRelationships(EntityTypeBuilder<Usuario> builder)
    {
        // Um usuario pode ser responsavel pela venda/cadastro de varios bilhetes.
        builder.HasMany(Usuario => Usuario.BilhetesVendidos)
            .WithOne(Bilhete => Bilhete.UsuarioResponsavel)
            .HasForeignKey(Bilhete => Bilhete.UsuarioResponsavelId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureIndexes(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasIndex(Usuario => Usuario.Email)
            .IsUnique();
    }
    private static void ConfigureSeeds(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasData(UsuarioSeed.Data);
    }
}
