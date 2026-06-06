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
                value: "pbkdf2_sha256$100000$cmlmYS1tYW5hZ2VyLWFkbWluLXNhbHQ=$Jwh4dZlB22rYR5uoofn97Y7FJwdnwOdI8KF8NzjssEM=");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Senha",
                value: "pbkdf2_sha256$100000$cmlmYS1tYW5hZ2VyLXVzZXItc2FsdA==$+cik2bvSeHUEaI/Mp22vmzb9ZZ+KwN/p/F63xTDcWVg=");
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
