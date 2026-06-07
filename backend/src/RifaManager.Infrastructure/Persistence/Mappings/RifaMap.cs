using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RifaManager.Domain.Entities;

namespace RifaManager.Infrastructure.Persistence.Mappings;

public sealed class RifaMap : IEntityTypeConfiguration<Rifa>
{
    public void Configure(EntityTypeBuilder<Rifa> builder)
    {
        ConfigureDataStructure(builder);
        ConfigureIndexes(builder);
    }

    private static void ConfigureDataStructure(EntityTypeBuilder<Rifa> builder)
    {
        builder.Property(Rifa => Rifa.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(Rifa => Rifa.Descricao)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(Rifa => Rifa.ValorBilhete)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(Rifa => Rifa.DataSorteio)
            .IsRequired();

        builder.Property(Rifa => Rifa.Premio)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(Rifa => Rifa.Encerrada)
            .IsRequired();
    }

    private static void ConfigureIndexes(EntityTypeBuilder<Rifa> builder)
    {
        builder.HasIndex(Rifa => Rifa.Nome);

        builder.HasIndex(Rifa => Rifa.DataSorteio);
    }
}
