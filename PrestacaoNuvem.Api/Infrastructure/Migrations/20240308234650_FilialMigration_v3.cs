using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrestacaoNuvem.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FilialMigration_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PrestadorId",
                table: "Filial",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Filial_PrestadorId",
                table: "Filial",
                column: "PrestadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Filial_Prestador_PrestadorId",
                table: "Filial",
                column: "PrestadorId",
                principalTable: "Prestador",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filial_Prestador_PrestadorId",
                table: "Filial");

            migrationBuilder.DropIndex(
                name: "IX_Filial_PrestadorId",
                table: "Filial");

            migrationBuilder.DropColumn(
                name: "PrestadorId",
                table: "Filial");
        }
    }
}
