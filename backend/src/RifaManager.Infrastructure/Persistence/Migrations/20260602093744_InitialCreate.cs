using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RifaManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Participantes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rifas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    ValorBilhete = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    DataSorteio = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Premio = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Encerrada = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rifas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Perfil = table.Column<int>(type: "INTEGER", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bilhetes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PagoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CanceladoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RifaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ParticipanteId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsuarioResponsavelId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bilhetes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bilhetes_Participantes_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bilhetes_Rifas_RifaId",
                        column: x => x.RifaId,
                        principalTable: "Rifas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bilhetes_Usuarios_UsuarioResponsavelId",
                        column: x => x.UsuarioResponsavelId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bilhetes_ParticipanteId",
                table: "Bilhetes",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Bilhetes_RifaId_Numero",
                table: "Bilhetes",
                columns: new[] { "RifaId", "Numero" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bilhetes_Status",
                table: "Bilhetes",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Bilhetes_UsuarioResponsavelId",
                table: "Bilhetes",
                column: "UsuarioResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Participantes_Nome",
                table: "Participantes",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_Participantes_Telefone",
                table: "Participantes",
                column: "Telefone");

            migrationBuilder.CreateIndex(
                name: "IX_Rifas_DataSorteio",
                table: "Rifas",
                column: "DataSorteio");

            migrationBuilder.CreateIndex(
                name: "IX_Rifas_Nome",
                table: "Rifas",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bilhetes");

            migrationBuilder.DropTable(
                name: "Participantes");

            migrationBuilder.DropTable(
                name: "Rifas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
