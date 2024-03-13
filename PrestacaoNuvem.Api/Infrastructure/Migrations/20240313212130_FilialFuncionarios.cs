using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrestacaoNuvem.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FilialFuncionarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FilialId",
                table: "FuncionarioPrestador",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioPrestador_FilialId",
                table: "FuncionarioPrestador",
                column: "FilialId");

            migrationBuilder.AddForeignKey(
                name: "FK_FuncionarioPrestador_Filial_FilialId",
                table: "FuncionarioPrestador",
                column: "FilialId",
                principalTable: "Filial",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuncionarioPrestador_Filial_FilialId",
                table: "FuncionarioPrestador");

            migrationBuilder.DropIndex(
                name: "IX_FuncionarioPrestador_FilialId",
                table: "FuncionarioPrestador");

            migrationBuilder.DropColumn(
                name: "FilialId",
                table: "FuncionarioPrestador");
        }
    }
}
