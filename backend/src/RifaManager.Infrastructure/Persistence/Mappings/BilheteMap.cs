using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RifaManager.Domain.Entities;

namespace RifaManager.Infrastructure.Persistence.Mappings;

public sealed class BilheteMap : IEntityTypeConfiguration<Bilhete>
{
    public void Configure(EntityTypeBuilder<Bilhete> builder)
    {
        ConfigureDataStructure(builder);
        ConfigureRelationships(builder);
        ConfigureIndexes(builder);
    }

    private static void ConfigureDataStructure(EntityTypeBuilder<Bilhete> builder)
    {
        builder.Property(Bilhete => Bilhete.Numero)
            .IsRequired();

        builder.Property(Bilhete => Bilhete.Status)
            .IsRequired();

        builder.Property(Bilhete => Bilhete.CriadoEm)
            .IsRequired();

        builder.Property(Bilhete => Bilhete.PagoEm)
            .IsRequired();

        builder.Property(Bilhete => Bilhete.CanceladoEm)
            .IsRequired();

        builder.Property(Bilhete => Bilhete.RifaId)
            .IsRequired();

        builder.Property(Bilhete => Bilhete.ParticipanteId)
            .IsRequired();

        builder.Property(Bilhete => Bilhete.UsuarioResponsavelId)
            .IsRequired();
    }

    private static void ConfigureRelationships(EntityTypeBuilder<Bilhete> builder)
    {
        // Cada bilhete pertence a uma rifa; a exclusao da rifa nao deve apagar historico de bilhetes.
        builder.HasOne(Bilhete => Bilhete.Rifa)
            .WithMany(Rifa => Rifa.Bilhetes)
            .HasForeignKey(Bilhete => Bilhete.RifaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Cada bilhete pertence a um participante; a exclusao do participante nao deve apagar seus bilhetes.
        builder.HasOne(Bilhete => Bilhete.Participante)
            .WithMany(Participante => Participante.Bilhetes)
            .HasForeignKey(Bilhete => Bilhete.ParticipanteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Cada bilhete registra o usuario responsavel pela venda/cadastro para fins de auditoria.
        builder.HasOne(Bilhete => Bilhete.UsuarioResponsavel)
            .WithMany(Usuario => Usuario.BilhetesVendidos)
            .HasForeignKey(Bilhete => Bilhete.UsuarioResponsavelId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureIndexes(EntityTypeBuilder<Bilhete> builder)
    {
        builder.HasIndex(Bilhete => new { Bilhete.RifaId, Bilhete.Numero })
            .IsUnique();

        builder.HasIndex(Bilhete => Bilhete.ParticipanteId);

        builder.HasIndex(Bilhete => Bilhete.UsuarioResponsavelId);

        builder.HasIndex(Bilhete => Bilhete.Status);
    }
}
