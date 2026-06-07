using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RifaManager.Domain.Entities;

namespace RifaManager.Infrastructure.Persistence.Mappings;

public sealed class ParticipanteMap : IEntityTypeConfiguration<Participante>
{
    public void Configure(EntityTypeBuilder<Participante> builder)
    {
        ConfigureDataStructure(builder);
        ConfigureIndexes(builder);
    }

    private static void ConfigureDataStructure(EntityTypeBuilder<Participante> builder)
    {
        builder.Property(Participante => Participante.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(Participante => Participante.Telefone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(Participante => Participante.Observacao)
            .HasMaxLength(500);
    }

    private static void ConfigureIndexes(EntityTypeBuilder<Participante> builder)
    {
        builder.HasIndex(Participante => Participante.Nome);

        builder.HasIndex(Participante => Participante.Telefone);
    }
}
