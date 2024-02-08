using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrestacaoNuvem.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FuncionarioOrdemServico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FuncionarioPrestadorId",
                table: "PrestacaoServico",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrestacaoServico_FuncionarioPrestadorId",
                table: "PrestacaoServico",
                column: "FuncionarioPrestadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrestacaoServico_FuncionarioPrestador_FuncionarioPrestadorId",
                table: "PrestacaoServico",
                column: "FuncionarioPrestadorId",
                principalTable: "FuncionarioPrestador",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrestacaoServico_FuncionarioPrestador_FuncionarioPrestadorId",
                table: "PrestacaoServico");

            migrationBuilder.DropIndex(
                name: "IX_PrestacaoServico_FuncionarioPrestadorId",
                table: "PrestacaoServico");

            migrationBuilder.DropColumn(
                name: "FuncionarioPrestadorId",
                table: "PrestacaoServico");
        }
    }
}
