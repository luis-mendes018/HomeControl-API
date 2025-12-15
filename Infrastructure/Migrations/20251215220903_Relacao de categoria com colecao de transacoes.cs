using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Relacaodecategoriacomcolecaodetransacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoriaId1",
                table: "Transacoes",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_CategoriaId1",
                table: "Transacoes",
                column: "CategoriaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Categorias_CategoriaId1",
                table: "Transacoes",
                column: "CategoriaId1",
                principalTable: "Categorias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Categorias_CategoriaId1",
                table: "Transacoes");

            migrationBuilder.DropIndex(
                name: "IX_Transacoes_CategoriaId1",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "CategoriaId1",
                table: "Transacoes");
        }
    }
}
