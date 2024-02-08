using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrestacaoNuvem.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Carro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Km",
                table: "Veiculo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataConclusaoServico",
                table: "PrestacaoServico",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Km",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "DataConclusaoServico",
                table: "PrestacaoServico");
        }
    }
}
