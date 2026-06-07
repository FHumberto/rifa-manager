using Microsoft.EntityFrameworkCore;
using RifaManager.Domain.Entities;

namespace RifaManager.Infrastructure.Persistence.Context;

public sealed class RifaDbContext(DbContextOptions<RifaDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Participante> Participantes { get; set; }
    public DbSet<Bilhete> Bilhetes { get; set; }
    public DbSet<Rifa> Rifas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RifaDbContext).Assembly);
    }
}
