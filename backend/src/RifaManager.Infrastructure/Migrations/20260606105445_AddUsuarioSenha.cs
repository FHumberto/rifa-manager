using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RifaManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioSenha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Usuarios",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Senha",
                value: "AQAAAAIAAYagAAAAEDOSuz2SoafpO/IZGEl9L9YYNI5ojnfT6KKEOmRTr8EYiJ/fOQvuCnJ8ilXHJy4sLw==");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Senha",
                value: "AQAAAAIAAYagAAAAEEy7ZCvRrfkE6u4+/ro1PagYUSkzb9aMHckterhHYCI2uThrCeoQrQLbCTtRK4Q8Zg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Usuarios");
        }
    }
}
