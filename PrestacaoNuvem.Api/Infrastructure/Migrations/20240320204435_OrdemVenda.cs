using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrestacaoNuvem.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrdemVenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "VendaOrdem",
                startValue: 1000L);

            migrationBuilder.AddColumn<Guid>(
                name: "OrdemVendaId",
                table: "Produto",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrdemVenda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Referencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FuncionarioPrestadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrestadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDesativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrCadastro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsrDesativacao = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdemVenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdemVenda_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdemVenda_FuncionarioPrestador_FuncionarioPrestadorId",
                        column: x => x.FuncionarioPrestadorId,
                        principalTable: "FuncionarioPrestador",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrdemVenda_Prestador_PrestadorId",
                        column: x => x.PrestadorId,
                        principalTable: "Prestador",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produto_OrdemVendaId",
                table: "Produto",
                column: "OrdemVendaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdemVenda_ClienteId",
                table: "OrdemVenda",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdemVenda_FuncionarioPrestadorId",
                table: "OrdemVenda",
                column: "FuncionarioPrestadorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdemVenda_PrestadorId",
                table: "OrdemVenda",
                column: "PrestadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_OrdemVenda_OrdemVendaId",
                table: "Produto",
                column: "OrdemVendaId",
                principalTable: "OrdemVenda",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_OrdemVenda_OrdemVendaId",
                table: "Produto");

            migrationBuilder.DropTable(
                name: "OrdemVenda");

            migrationBuilder.DropIndex(
                name: "IX_Produto_OrdemVendaId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "OrdemVendaId",
                table: "Produto");
        }
    }
}
